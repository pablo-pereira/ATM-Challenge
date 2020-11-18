import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Menu } from './components/Menu';
import { Pin } from './components/Pin';


import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/pin' component={Pin} />
        <Route path='/counter' component={Menu} />
        <Route path='/fetch-data' component={FetchData} />
      </Layout>
    );
  }
}
