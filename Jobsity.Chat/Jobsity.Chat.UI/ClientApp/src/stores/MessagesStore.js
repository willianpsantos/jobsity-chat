export default class MessagesStore
{
    static get storeName() {
        return 'messages';
    }

    static get dbName() {
        return 'messagesDB';
    }

    static get store() {
        return MessagesStore._store;
    }

    static set store(value) {
        MessagesStore._store = value;
    }

    static get opened() {
        return MessagesStore._opened;
    }

    static set opened(value) {
        MessagesStore._opened = value;
    }

    static get isIndexesCreated() {
        return MessagesStore._isIndexesCreated;
    }

    static set isIndexesCreated(value) {
        MessagesStore._isIndexesCreated = value;
    }


    static open() {
        return new Promise((resolve, reject) => {
            const currentVersion = 1;

            if (this.opened) {
                resolve({ store: this.store });
                return;
            }

            const db = window.indexedDB.open(this.dbName, currentVersion);

            db.onerror = (e) => {
                this.opened = false;
                alert('Error on loading database');
                console.log(e);
                reject(e);
            }

            db.onsuccess = (e) => {
                console.info('Database opened successfully');

                this.store = e.target.result;
                this.opened = true;
                resolve({ event: e, store: this.store });
            }

            db.onupgradeneeded = (e) => {
                const result = e.target.result;
                
                if ( result.objectStoreNames.contains(this.storeName) ) {
                    result.deleteObjectStore(this.storeName);
                }
                
                this.store = result.createObjectStore(this.storeName, { keyPath: 'id', autoIncrement: false });
                this.opened = true;

                if (!this.isIndexesCreated) {
                    this.store.createIndex('userId', 'userId', { unique: false });
                    this.store.createIndex('chatRoomId', 'chatRoomId', { unique: false });

                    this.isIndexesCreated = true;
                }

                resolve({ event: e, store: this.store });
            } 
        });
    }

    static saveMessage(message) {
        return new Promise((resolve, reject) => {
            if (!this.store || !this.store.transaction) {
                resolve({ event: undefined, data: message });
                return;
            }

            const transaction = this.store.transaction(this.storeName, 'readwrite');

            transaction.oncomplete = (e) => {
                console.info('Transaction opened successfully');
            }

            transaction.onerror = (e) => {
                alert('Error on open transaction');
                console.log(e);
                reject(e);
            }

            const store = transaction.objectStore(this.storeName);
            let request = store.get(message.id);

            request.onerror = function (e) {
                console.log('Error on saving message.');
                reject(e);
            }        
            
            request.onsuccess = function (e) {
                const result = e.target.result;

                if(!result) {
                    store.add(message);
                    resolve({ event: e, data: message });
                    return;
                }

                for(let prop in message) {
                    result[prop] = message[prop];
                }

                store.put(result);
                resolve({ event: e, data: result });
            }
        });
    }

    static getMessages(filter) {
        return new Promise((resolve, reject) => {
            const transaction = this.store.transaction(this.storeName, 'readonly');            

            transaction.oncomplete = (e) => {
                console.info('Transaction opened successfully');
                resolve(e);
            }

            transaction.onerror = (e) => {
                alert('Error on open transaction');
                console.log(e);
                reject(e);
            }

            let indexName = "";
            let range = undefined;

            if (filter) {
                if('chatRoomId' in filter) {
                    indexName = 'chatRoomId';
                }
                else if ('userId' in filter) {
                    indexName = 'userId';
                }

                range = IDBKeyRange.only(filter[indexName]);
            }

            const store = transaction.objectStore(this.storeName);
            const cursor = indexName ? store.index(indexName).openCursor(range) : store.openCursor(range);
            const data = [];

            cursor.oncomplete = (e) => {
                resolve({ event: e, data });
            };
            
            cursor.onsuccess = (e) => {                
                const c = e.target.result;

                if (!c) {         
                    resolve({ event: e, data });           
                    return;
                }

                data.push(c.value);
                c.continue();
            };
        });
    }
}