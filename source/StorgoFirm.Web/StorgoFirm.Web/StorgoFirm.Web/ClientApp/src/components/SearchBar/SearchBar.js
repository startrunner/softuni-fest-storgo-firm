import _ from "lodash";
import faker from "faker";
import React, { Component } from "react";
import { Search, Grid, Header, Segment } from "semantic-ui-react";
const req =  require('./../../helpers/fetch.js');
const get = req.get;

const initialState = { isLoading: false, results: [], value: "" };


export default class SearchExampleStandard extends Component {

  constructor (props){
    super(props);
    this.state={

    }
  }

  componentDidMount(){
  }

  state = initialState;

  getData = async ()=>{
    const data = await get('/')
  }

  handleResultSelect = (e, { result }) =>
    this.setState({ value: result.eventName });

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
      <Search
        loading={isLoading}
        onResultSelect={this.handleResultSelect}
        onSearchChange={_.debounce(this.handleSearchChange, 500, {
          leading: true
        })}
        results={results}
        value={value}
        {...this.props}
      />
    );
  }
}
