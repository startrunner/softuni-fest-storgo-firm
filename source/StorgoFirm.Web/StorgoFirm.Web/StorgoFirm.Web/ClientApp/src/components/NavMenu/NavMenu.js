import React from "react";
import {
    Button,
    Container,
    Menu,
    Segment,
    Visibility,
    Image,
    Popup
} from "semantic-ui-react";
import { Link } from "react-router-dom";
import SearchBar from "../SearchBar/SearchBar";
import "./NavMenu.css";

export default class NavMenu extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            user: props.user,
            authenticating: props.authenticating
        };
    }

    hideFixedMenu = () => this.setState({ fixed: false });
    showFixedMenu = () => this.setState({ fixed: true });

    render() {
        const fixed = this.state.fixed;
        const authenticating = this.state.authenticating;
        const user = this.state.user;
        if (authenticating) {
            return (
                <Visibility
                    once={false}
                    onBottomPassed={this.showFixedMenu}
                    onBottomPassedReverse={this.hideFixedMenu}
                    className="dekstop-header"
                >
                    <Segment
                        inverted
                        textAlign="center"
                        vertical
                        className="dekstopheader"
                    >
                        <Menu
                            fixed={fixed ? "top" : null}
                            inverted={!fixed}
                            pointing={!fixed}
                            secondary={!fixed}
                            size="large"
                            className="dekstopheader-menu"
                            style={{ padding: "0em 0em" }}
                        >
                            <Container>
                                <Menu.Item as={Link} to="/" style={{ padding: "0em 0em" }} />
                            </Container>
                        </Menu>
                    </Segment>
                </Visibility>
            );
        } else {
            if (user) {
                return (
                    <Visibility
                        once={false}
                        onBottomPassed={this.showFixedMenu}
                        onBottomPassedReverse={this.hideFixedMenu}
                        className="dekstop-header"
                    >
                        <Segment
                            inverted
                            textAlign="center"
                            vertical
                            className="dekstopheader"
                        >
                            <Menu
                                fixed={fixed ? "top" : null}
                                inverted={!fixed}
                                pointing={!fixed}
                                secondary={!fixed}
                                size="large"
                                className="dekstopheader-menu"
                                style={{ padding: "0em 0em" }}
                            >
                                <Container>
                                    <Menu.Item as={Link} to="/" style={{ padding: "0em 0em" }} />
                                    <Menu.Item position="right">
                                        <Button
                                            as={Link}
                                            to="/timeline"
                                            inverted={fixed}
                                            style={{ marginLeft: "0.5em" }}
                                            className="dekstopheader-button"
                                            color="gray"
                                        >
                                            Дневник
                    </Button>
                                        <Button
                                            as={Link}
                                            to="/profile"
                                            inverted={fixed}
                                            style={{ marginLeft: "0.5em" }}
                                            className="dekstopheader-button"
                                            color="gray"
                                        >
                                            Профил
                    </Button>
                                        <Button
                                            onClick={this.handleSignOut}
                                            inverted={fixed}
                                            style={{ marginLeft: "0.5em" }}
                                            className="dekstopheader-button"
                                            color="gray"
                                        >
                                            Изход
                    </Button>
                                    </Menu.Item>
                                </Container>
                            </Menu>
                        </Segment>
                    </Visibility>
                );
            } else {
                return (
                    <Visibility
                        once={false}
                        onBottomPassed={this.showFixedMenu}
                        onBottomPassedReverse={this.hideFixedMenu}
                        className="dekstop-header"
                    >
                        <Segment
                            inverted
                            textAlign="center"
                            vertical
                            className="dekstopheader"
                        >
                            <Menu
                                fixed={fixed ? "top" : null}
                                inverted={!fixed}
                                pointing={!fixed}
                                secondary={!fixed}
                                size="large"
                                className="dekstopheader-menu"
                                style={{ padding: "0em 0em" }}
                            >
                                <Container>
                                    <Menu.Item as={Link} to="/" style={{ padding: "0em 0em" }} />
                                    <Menu.Item>
                                        <SearchBar events={this.props.events} isAdmin={this.props.isAdmin} />
                                    </Menu.Item>
                                    <Menu.Item position="right" style={{ padding: "1em 0em" }}>
                                        <Popup
                                            trigger={
                                                <Button
                                                    inverted={fixed}
                                                    primary={fixed}
                                                    className="dekstopheader-button"
                                                    color="gray"
                                                >
                                                    Вход
                        </Button>
                                            }
                                            basic
                                            on="click"
                                        />
                                        <Button
                                            as={Link}
                                            to="/signup"
                                            inverted={fixed}
                                            primary={fixed}
                                            style={{ marginLeft: "0.5em" }}
                                            className="dekstopheader-button"
                                            color="gray"
                                        >
                                            Регистрация
                    </Button>
                                    </Menu.Item>
                                </Container>
                            </Menu>
                        </Segment>
                    </Visibility>
                );
            }
        }
    }
}
