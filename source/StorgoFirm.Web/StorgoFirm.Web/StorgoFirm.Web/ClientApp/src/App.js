import React, { Component } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout/Layout";
import { Home } from "./components/Home/Home";
import { FetchData } from "./components/FetchData/FetchData";
import { Counter } from "./components/Counter/Counter";
import './App.css';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <div className="app-window">
        <link
          rel="stylesheet"
          href="//cdn.jsdelivr.net/npm/semantic-ui@2.4.2/dist/semantic.min.css"
        />
        <Layout>
          <Route exact path="/" component={Home} />
          <Route path="/counter" component={Counter} />
          <Route path="/fetch-data" component={FetchData} />
        </Layout>
      </div>
    );
  }
}
