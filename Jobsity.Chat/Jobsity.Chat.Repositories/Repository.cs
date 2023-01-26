using Jobsity.Chat.Core;
using Jobsity.Chat.Core.Attributes;
using Jobsity.Chat.Core.Interfaces;
using Jobsity.Chat.DB;
using Jobsity.Chat.Interfaces.Repositories;
using Jobsity.Chat.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Jobsity.Chat.Repositories
{
    public class Repository<TEntity, TPredicateReturnType> : IRepository<TEntity, TPredicateReturnType>
        where TEntity : JobsityModel
        where TPredicateReturnType : IRepository<TEntity, TPredicateReturnType>
    {
        protected const int DEFAULT_PAGE_SIZE = 50;

        protected readonly JobsityChatDataContext _context;
        protected Expression<Func<TEntity, bool>>? _predicate;
        protected ICollection<Expression>? _includes;
        protected ICollection<Expression<Func<TEntity, object>>>? _orderByAscList;
        protected ICollection<Expression<Func<TEntity, object>>>? _orderByDescList;


        public Repository(JobsityChatDataContext context) 
        {
            _context = context;
            _includes = new HashSet<Expression>();
            _orderByAscList = new HashSet<Expression<Func<TEntity, object>>>();
            _orderByDescList = new HashSet<Expression<Func<TEntity, object>>>();
        }


        private string[] GetPropertiesCanNotBeUpdated()
        {
            var properties = typeof(TEntity).GetProperties();
            var navPropNames = new HashSet<string>();

            foreach (var prop in properties)
            {
                var attrs = prop.GetCustomAttribute<CanBeUpdated>(true);

                if (attrs is null)
                {                   
                    continue;
                }

                if(attrs.Can)
                {
                    continue;
                }

                navPropNames.Add(prop.Name);
            }

            return navPropNames.ToArray();
        }

        private void AssignUpdatableProps(ref TEntity original, ref TEntity updated, string[] except)
        {
            var typeOfEntity = typeof(TEntity);
            var typeOfJobsityModel = typeof(JobsityModel);
            var properties = typeof(TEntity).GetProperties();

            foreach (var prop in properties)
            {
                if (prop.Name == "CreatedAt" || prop.Name == "CreatedBy")
                {
                    continue;
                }

                if(except is not null && except.Length > 0)
                {
                    if (except.Contains(prop.Name))
                        continue;
                }

                var propType = prop.PropertyType;

                if (propType.BaseType != null && (propType.BaseType.Equals(typeOfJobsityModel) || propType.BaseType.Equals(typeOfEntity)))
                {
                    continue;
                }

                var originalValue = prop.GetValue(original);
                var updatedValue = prop.GetValue(updated);

                if (updatedValue == originalValue)
                {
                    continue;
                }

                prop.SetValue(original, updatedValue);
            }
        }

        private void SetIncludes(ref IQueryable<TEntity> query)
        {
            if (_includes is null || _includes.Count == 0)
            {
                return;
            }

            foreach (var include in _includes)
            {
                var type = include.GetType();
                var memberExpression = Convert.ChangeType(include, type);

                if (memberExpression is null)
                    continue;

                var bodyProp = type.GetProperty("Body");

                if (bodyProp is null)
                    continue;

                var body = (MemberExpression)bodyProp?.GetValue(memberExpression);

                if (body is null)
                    continue;

                var name = body?.Member?.Name;

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
                    query = query.Include(name);
            }
        }

        private void SetOrderBy(ref IQueryable<TEntity> query)
        {
            if (_orderByAscList is null || _orderByAscList.Count == 0)
            {
                return;
            }

            foreach (var order in _orderByAscList)
            {
                query = query.OrderBy(order);
            }
        }

        private void SetOrderByDescending(ref IQueryable<TEntity> query)
        {
            if (_orderByDescList is null || _orderByDescList.Count == 0)
            {
                return;
            }

            foreach (var order in _orderByDescList)
            {
                query = query.OrderBy(order);
            }
        }


        public virtual TPredicateReturnType Where(Expression<Func<TEntity, bool>> predicate)
        {
            _predicate = predicate;
            IRepository<TEntity, TPredicateReturnType> repository = this;
            return (TPredicateReturnType)repository;
        }

        public TPredicateReturnType Include<TProperty>(Expression<Func<TEntity, TProperty>> member) where TProperty : class
        {
            _includes.Add(member);
            IRepository<TEntity, TPredicateReturnType> repository = this;
            return (TPredicateReturnType)repository;
        }

        public TPredicateReturnType OrderBy(Expression<Func<TEntity, object>> member)
        {
            _orderByAscList?.Add(member);
            IRepository<TEntity, TPredicateReturnType> repository = this;
            return (TPredicateReturnType)repository;
        }

        public TPredicateReturnType OrderByDescending(Expression<Func<TEntity, object>> member)
        {
            _orderByDescList?.Add(member);
            IRepository<TEntity, TPredicateReturnType> repository = this;
            return (TPredicateReturnType)repository;
        }


        public async Task<TEntity?> AddAsync(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var set = _context.Set<TEntity>();

            if(entity.Id.CompareTo(Guid.Empty) == 0)
            {
                entity.Id = Guid.NewGuid();
            }

            set.Attach(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity?> DeleteAsync(Guid id)
        {
            if (id.CompareTo(Guid.Empty) == 0)
            {
               throw new ArgumentOutOfRangeException(nameof(id));
            }

            var set = _context.Set<TEntity>();
            var entity = await set.FindAsync(id);

            set.Attach(entity).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity?> FindByIdAsync(Guid id)
        {
            if (id.CompareTo(Guid.Empty) == 0)
            {
                return null;
            }

            var set = _context.Set<TEntity>();
            var entity = await set.FindAsync(id);

            return entity;
        }

        public async Task<IEnumerable<TEntity>?> GetAsync()
        {
            var set = _context.Set<TEntity>();
            IEnumerable<TEntity>? result = null;
            IQueryable<TEntity> query = set.AsQueryable();

            SetIncludes(ref query);

            if (_predicate is not null)
                query = query.Where(_predicate);

            SetOrderBy(ref query);
            SetOrderByDescending(ref query);

            result = await query.ToArrayAsync();

            _includes?.Clear();
            _orderByAscList?.Clear();
            _orderByDescList?.Clear();
            _predicate = null;

            return result;
        }

        public async Task<IPageableResponse<TEntity>?> GetPaginatedAsync(int page, int pageSize)
        {
            if (page == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page));
            }

            var set = _context.Set<TEntity>();
            IQueryable<TEntity> query = set.AsQueryable();
            IQueryable<TEntity>? pagedQuery = null;            

            IPageableResponse<TEntity> response = new PageableResponse<TEntity>
            {
                Page = page,
                PageSize = pageSize > 0 ? pageSize : DEFAULT_PAGE_SIZE
            };

            IEnumerable<TEntity> data = null;
            var count = 0;

            SetIncludes(ref query);

            if (_predicate is not null)
            {
                count = await query.Where(_predicate).CountAsync();

                pagedQuery = 
                    query
                        .Where(_predicate)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize);
            }
            else
            {
                count = await query.CountAsync();

                pagedQuery = 
                    query                
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize);
            }

            SetOrderBy(ref pagedQuery);
            SetOrderByDescending(ref pagedQuery);

            data = await pagedQuery.ToArrayAsync();

            _includes?.Clear();
            _orderByAscList?.Clear();
            _orderByDescList?.Clear();
            _predicate = null;

            response.Count = count;
            response.Data = data;

            return response;
        }

        public async Task<TEntity?> UpdateAsync(Guid id, TEntity entity)
        {
            if (id.CompareTo(Guid.Empty) == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var set = _context.Set<TEntity>();
            var except = GetPropertiesCanNotBeUpdated();
            var original = await set.FindAsync(id);

            if (original is null)
            {
                throw new NullReferenceException(nameof(original));
            }

            AssignUpdatableProps(ref original, ref entity, except);

            await _context.SaveChangesAsync();

            return original;
        }
    }
}