import React, { Component } from "react";
import { Grid, Table, TableHeader, TableHeaderCell, TableBody, TableRow, Button, Header, Icon, TableCell, Label, Segment, Modal, Image } from "semantic-ui-react";
import "./Home.css";
const moment = require("moment");

let parseUtcDate = function (utcString) {
    let utcTime = moment.utc(utcString/*, "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"*/);
    let localTime = utcTime.toDate();
    //return localTime.toString();
    return localTime.toLocaleDateString();//("ddd-Do-Mo-YYYY");
}


const FORMAT_DECIMAL = 1;
const FORMAT_AMERICAN = 2;

let getFraction = (decimal) => {
    var denominator = 1;
    while (Math.abs((decimal * denominator) % 1) < .1) {
        denominator++;
    }
    let numerator = decimal * denominator;
    return `${numerator - numerator % 1}/${denominator}`;
}

let formatOdd = function (odd, format) {
    if (format == FORMAT_DECIMAL) return odd;
    return getFraction(odd);
}

export class Home extends Component {
    constructor(props) {
        super(props);
        this.state = {
            curSport: null,
            curLeague: null,
            curFormat: FORMAT_DECIMAL
        };
    }

    componentDidMount() {
        const events = this.props.events;
        const sports = this.props.sports;
        const leagues = this.props.leagues;
    }

    handleLeagueChange(leagueId) {

    }

    handleSportChange(sportId) {
        let curSport = sportId;
        this.setState({ curSport });
    }

    handleFormatChange(format) {
        let curFormat = format;
        this.setState({ curFormat });
    }

    handleOpen = (event) => {
        this.setState({ openEvent: event });
        this.setState({ modalOpen: true });
    }
    handleClose = (event) => {
        this.setState({ openEvent: null });
        this.setState({ modalOpen: false });
    }

    static displayName = Home.name;

    render() {
        const events = this.props.events;
        const sports = this.props.sports;
        const currentSport = this.state.curSport;
        const currentFormat = this.state.curFormat;
        return (
            <Grid columns={3} className="home-grid">
                {events.length > 0 ?
                    <Grid.Row className="home-grid-row">
                        <Grid.Column width={3} className="home-grid-sports">
                            <Segment className="home-grid-sports">
                                <Header>Format</Header>
                                <Button basic onClick={() => this.handleFormatChange(FORMAT_DECIMAL)}>Decimal</Button>
                                <Button basic onClick={() => this.handleFormatChange(FORMAT_AMERICAN)}>American</Button>

                                <Header>Sports</Header>
                                {
                                    sports.map(sport => {
                                        return (
                                            <div>
                                                <Button basic onClick={() => this.handleSportChange(sport.id)}>{sport.name}</Button>
                                            </div>
                                        );
                                    })
                                }

                                <Header>Leagues</Header>
                                {

                                }
                            </Segment>
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
                                            <Table.HeaderCell></Table.HeaderCell>
                                        </Table.Row>
                                    </Table.Header>
                                    <Table.Body>
                                        <Modal className="grid-event-modal"
                                            open={this.state.modalOpen}
                                            onClose={this.handleClose}
                                        >
                                            <Modal.Header>Select a Photo</Modal.Header>
                                            <Modal.Content image>
                                                <Image wrapped size='medium' src='https://react.semantic-ui.com/images/avatar/large/rachel.png' />
                                                <div>
                                                    <Label>Home Team Score: </Label>{this.state.openEvent ? this.state.openEvent.homeTeamScore : null}
                                                </div>
                                                <div>
                                                    <Label>Away Team Score: </Label>{this.state.openEvent ? this.state.openEvent.awayTeamScore : null}
                                                </div>
                                                <div>
                                                    <Label>Home Team Odds: </Label>{this.state.openEvent ? formatOdd(this.state.openEvent.homeTeamOdds, currentFormat) : null}
                                                </div>
                                                <div>
                                                    <Label>Away Team Odds: </Label>{this.state.openEvent ? formatOdd(this.state.openEvent.awayTeamOdds, currentFormat) : null}
                                                </div>
                                                <div>
                                                    <Label>Draw Odds: </Label>{this.state.openEvent ? formatOdd(this.state.openEvent.drawOdds, currentFormat) : null}
                                                </div>
                                            </Modal.Content>
                                            <Modal.Actions>
                                                <Button color='green' onClick={this.handleClose} inverted>
                                                    <Icon name='checkmark' /> Got it
                                                                        </Button>
                                            </Modal.Actions>
                                        </Modal>
                                        {
                                            events.
                                                filter(x => currentSport == null || x.sport.id == currentSport).
                                                map(event => {
                                                    return (
                                                        <Table.Row >
                                                            <Table.Cell>{event.eventName}</Table.Cell>
                                                            <Table.Cell>{parseUtcDate(event.eventDate)}</Table.Cell>
                                                            <Table.Cell>{event.league.name}</Table.Cell>
                                                            <Table.Cell>{event.sport.name}</Table.Cell>
                                                            <Table.Cell>
                                                                <Button onClick={() => this.handleOpen(event)}>View</Button>
                                                            </Table.Cell>
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

