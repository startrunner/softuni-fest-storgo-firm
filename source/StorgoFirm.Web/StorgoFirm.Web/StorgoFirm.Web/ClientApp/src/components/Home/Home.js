import React, { Component } from "react";
import { Grid, Table, TableHeader, TableHeaderCell, TableBody, TableRow, TableCell, Label, Segment, Image } from "semantic-ui-react";
import "./Home.css";
const moment = require("moment");

let parseUtcDate = function (utcString) {
    let utcTime = moment.utc(utcString/*, "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"*/);
    let localTime = utcTime.toDate();
    //return localTime.toString();
    return localTime.toLocaleDateString();//("ddd-Do-Mo-YYYY");
}

export class Home extends Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    static displayName = Home.name;

    render() {
        const events = this.props.events;
        const sports = this.props.sports;
        return (
            <Grid columns={3} className="home-grid">
                {events.length > 0 ?
                    <Grid.Row className="home-grid-row">
                        <Grid.Column width={3} className="home-grid-sports">
                            <Segment className="home-grid-sports" />
                        </Grid.Column>
                        <Grid.Column width={11} className="home-grid-main">
                            <Segment className="home-grid-main" >
                                <Table>
                                    <Table.Header>
                                        <Table.Row>
                                            <Table.HeaderCell>Event Name</Table.HeaderCell>
                                            <Table.HeaderCell>Date</Table.HeaderCell>
                                            <Table.HeaderCell>League</Table.HeaderCell>
                                            <Table.HeaderCell>Sport</Table.HeaderCell>
                                        </Table.Row>
                                    </Table.Header>
                                    <Table.Body>
                                        {

                                            events.map(event => {
                                                return (
                                                    <Table.Row>
                                                        <Table.Cell>{event.eventName}</Table.Cell>
                                                        <Table.Cell>{parseUtcDate(event.eventDate)}</Table.Cell>
                                                        <Table.Cell>{event.league.name}</Table.Cell>
                                                        <Table.Cell>{event.sport.name}</Table.Cell>
                                                    </Table.Row>
                                                );
                                            })
                                        }
                                    </Table.Body>
                                </Table>
                            </Segment>
                        </Grid.Column>
                        <Grid.Column width={2} className="home-grid-ads">
                            <Image src={require("./../../public/sportsbanner.jpg")} />
                        </Grid.Column>
                    </Grid.Row>
                    : null}
            </Grid>
        );
    }
}

