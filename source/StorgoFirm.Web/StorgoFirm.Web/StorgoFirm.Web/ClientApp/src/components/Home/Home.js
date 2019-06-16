import React, { Component } from "react";
import { Grid, Segment, Image } from "semantic-ui-react";
import "./Home.css";



export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <Grid columns={3} className="home-grid">
        <Grid.Row className="home-grid-row">
          <Grid.Column width={3} className="home-grid-sports">
            <Segment className="home-grid-sports" />
          </Grid.Column>
          <Grid.Column width={11} className="home-grid-main">
            <Segment  className="home-grid-main"/>
          </Grid.Column>
          <Grid.Column width={2} className="home-grid-ads">
            <Image src={require('./../../public/sportsbanner.jpg')}>

            </Image>
          </Grid.Column>
        </Grid.Row>
      </Grid>
    );
  }
}
