import _ from "lodash";
import PropTypes from 'prop-types';
import React, { Component } from "react";
import { Search, Grid, Header, Segment, Label, Button,Modal } from "semantic-ui-react";
import { withRouter } from 'react-router-dom'
const req = require('./../../helpers/fetch.js');
const get = req.get;


export default class EventModal extends Component {

    constructor(props) {
        super(props);
        this.state = {

        }
    }


    handleResultSelect = (e, { result }) => {
        const currSport = result.sport.name;
        const currLeague = result.league.name;
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
            <Modal trigger={<Button>Show Modal</Button>}>
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
        );
    }
}
