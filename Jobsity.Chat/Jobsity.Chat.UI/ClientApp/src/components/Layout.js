import React, { Component } from 'react';
import { useLocation } from 'react-router-dom';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export function Layout(props) {  
    const location = useLocation();
    Layout.displayName = props.name;  

    return (
      <div>
        {(location.pathname !== '/login' ? <NavMenu /> : null)}
        
        <main className='container-fluid'>
          {props.children}
        </main>
      </div>
    );
};
