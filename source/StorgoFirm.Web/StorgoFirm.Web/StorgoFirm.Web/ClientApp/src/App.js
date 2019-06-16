import React, { Component } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout/Layout";
import { Home } from "./components/Home/Home";
import { FetchData } from "./components/FetchData/FetchData";
import { Counter } from "./components/Counter/Counter";
import './App.css';

const req = require('./helpers/fetch.js');
const get = req.get;

export default class App extends Component {

    constructor(props) {
        super(props);
        this.state = {
            events: []
        }
    }

    static displayName = App.name;

    getData = async () => {
        const events = await (await get("/api/events/List")).json();
        const sports = await (await get("/api/Sports/List")).json();
        this.setState({ events, sports });
    }

    componentDidMount() {
        this.getData();
    }

    render() {
        const events = this.state.events;
        const sports = this.state.sports;
        return (
            <div className="app-window">
                <link
                    rel="stylesheet"
                    href="//cdn.jsdelivr.net/npm/semantic-ui@2.4.2/dist/semantic.min.css"
                />
                <Layout events={events} >
                    <Route exact path="/" render={(props) => <Home {...props} isAuthed={true} events={events} sports={sports} />} />
                    <Route path="/counter" render={(props) => <Counter {...props} isAuthed={true} />} />
                    <Route path="/fetch-data" render={(props) => <FetchData {...props} isAuthed={true} />} />
                </Layout>
            </div>
        );
    }
}
