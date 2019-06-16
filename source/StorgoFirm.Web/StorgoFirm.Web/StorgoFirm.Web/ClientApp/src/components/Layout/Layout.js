import React, { Component } from "react";
import { Container } from "reactstrap";
import NavMenu from "../NavMenu/NavMenu";
import "./Layout.css";

export class Layout extends Component {
  constructor(props) {
    super(props);
  }

  static displayName = Layout.name;

  render() {
    const events = this.props.events;
    return (
      <div className="layout-container">
        <NavMenu  events={events} />
        <Container className="layout-container">
          {this.props.children}
        </Container>
      </div>
    );
  }
}
