import React from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import Home  from './containers/Home';
import CreateBooking from './containers/CreateBooking';
import Result from './containers/Results'
import CreateSpace from './containers/CreateSpace'
import Summary from "./components/Summary"




import './custom.css'

export default function App() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/home' component={Home} />
        <Route exact path='/createspace' component={CreateSpace} />
        <Route exact path="/createbooking" component={CreateBooking} />
        <Route exact path='/results' component={Result} />
        <Route exact path ='/summary' component={Summary} /> 
      </Layout>
    );
}
