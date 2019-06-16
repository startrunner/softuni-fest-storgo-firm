import _ from "lodash";
import PropTypes from 'prop-types';
import React, { Component } from "react";
import { Search, Grid, Header, Segment, Label, Modal, Image } from "semantic-ui-react";
import { withRouter } from 'react-router-dom'
const req = require('./../../helpers/fetch.js');
const get = req.get;

const initialState = { isLoading: false, results: [], value: "" };

const resultRenderer = ({ eventName }) => <Modal trigger={<Label>{eventName}</Label>}>
    <Modal.Header>Select a Photo</Modal.Header>
    <Modal.Content image>
        <Image wrapped size='medium' src='/images/avatar/large/rachel.png' />
        <Modal.Description>
            <Header>Default Profile Image</Header>
            <p>We've found the following gravatar image associated with your e-mail address.</p>
            <p>Is it okay to use this photo?</p>
        </Modal.Description>
    </Modal.Content>
</Modal>


resultRenderer.propTypes = {
    title: PropTypes.string,
    description: PropTypes.string,
}

export default class SearchExampleStandard extends Component {

    constructor(props) {
        super(props);
        this.state = {

        }
    }

    componentDidMount() {
    }

    state = initialState;

    getData = async () => {
        const data = await get('/')
    }

    handleResultSelect = (e, { result }) => {
        const currSport = result.sport.name;
        const currLeague = result.league.name;
        this.setState({ result })
        //window.location.assign(`/event/${currSport}/${currLeague}/${result.eventName}`);
        //this.setState({ value: window.location.href});
    }

    handleSearchChange = (e, { value }) => {
        this.setState({ isLoading: true, value });

        setTimeout(() => {
            if (this.state.value.length < 1) return this.setState(initialState);

            const re = new RegExp(_.escapeRegExp(this.state.value), "i");
            const isMatch = result => re.test(result.eventName);

            this.setState({
                isLoading: false,
                results: _.filter(this.props.events, isMatch)
            });
        }, 300);
    };

    render() {
        const { isLoading, value, results } = this.state;
        const events = this.props.events;
        return (
            <Grid>
                <Grid.Column width={6}>
                    {this.props.isAdmin}
                    <Search
                        loading={isLoading}
                        onResultSelect={this.handleResultSelect}
                        onSearchChange={_.debounce(this.handleSearchChange, 500, {
                            leading: true,
                        })}
                        results={results}
                        value={value}
                        resultRenderer={resultRenderer}
                        {...this.props}
                    />
                </Grid.Column>
            </Grid>
        );
    }
}
