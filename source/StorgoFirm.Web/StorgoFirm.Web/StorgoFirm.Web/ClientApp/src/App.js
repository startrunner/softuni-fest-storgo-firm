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
            events: [],
            isLoading: true
        }
    }

    static displayName = App.name;

    getData = async () => {
        this.setState({ isLoading: true });
        const events = await (await get("/api/events/List")).json();
        const sports = await (await get("/api/Sports/List")).json();
        const leagues = await (await get("api/Leagues/List")).json();
        this.setState({ events, sports, leagues });
        this.setState({ isLoading: false });
    }

    componentDidMount() {
        this.getData();
    }

    render() {
        const events = this.state.events;
        const sports = this.state.sports;
        const leagues = this.state.leagues;
        return (
            <div className="app-window">
                <link
                    rel="stylesheet"
                    href="//cdn.jsdelivr.net/npm/semantic-ui@2.4.2/dist/semantic.min.css"
                />
                <Route exact path="/" render={(props) =>
                    <Layout events={events} >
                        <Home {...props} isAuthed={true} events={events} sports={sports} leagues={leagues} isLoading={this.state.isLoading} />
                    </Layout>
                } />
                <Route exact path="/admin" render={(props) =>
                    <Layout events={events} isAdmin={true} >
                        <Home {...props} isAuthed={true} events={events} sports={sports} leagues={leagues} isAdmin={true} isLoading={this.state.isLoading} />
                    </Layout>
                } />
            </div>
        );
    }
}
