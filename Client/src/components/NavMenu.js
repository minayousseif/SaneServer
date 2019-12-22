import * as React from 'react';
import {  
    Container, 
    Nav, 
    Navbar, 
    NavbarBrand, 
    NavItem, 
    NavLink,
    UncontrolledDropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem
} from 'reactstrap';
import { Link } from 'react-router-dom';
import { AuthCookie } from '../utils/AuthHelper';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { 
    faUserCircle,
    faUserCog,
    faCogs,
    faSignOutAlt,
    faPrint
} from '@fortawesome/free-solid-svg-icons';

import './NavMenu.scss';

export default class NavMenu extends React.PureComponent {
    constructor (props) {
        super(props);

        this.state = {
            show: false,
            isOpen: false,
        };
    }

    componentDidMount() {
        const user = AuthCookie.get();
        if (user) {
            console.log(user);
            this.setState({ show: true});
        }
    }

    render() {
        return (
            this.state.show &&
            <header>
                <Navbar className="topbar navbar-expand-sm bg-primary navbar-toggleable-sm border-bottom box-shadow mb-4 shadow" light>
                    {/* <Container>
                        <NavbarBrand tag={Link} to="/"></NavbarBrand>
                        <NavbarToggler onClick={this.toggle} className="mr-2"/>
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={this.state.isOpen} navbar>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/counter">Counter</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/fetch-data">Fetch data</NavLink>
                                </NavItem>
                            </ul>
                        </Collapse>
                    </Container> */}
                    <Container fluid={true} >
                        <NavbarBrand tag={Link} to="/" className="text-white">
                            <FontAwesomeIcon icon={faPrint} size="1x" color="fw" /> <b>Sane Server</b>
                        </NavbarBrand>
                        <Navbar color="faded" light>
                            <Nav className="flex-grow ml-auto" navbar>
                                <UncontrolledDropdown nav inNavbar>
                                    <DropdownToggle nav>
                                        <FontAwesomeIcon icon={faUserCircle} size="2x" className="fw text-white"/>
                                    </DropdownToggle>
                                    <DropdownMenu right className="shadow animated--grow-in">
                                        <DropdownItem>
                                            <FontAwesomeIcon 
                                                icon={faUserCog} 
                                                size="sm" color="fw" 
                                                className="mr-2 text-gray-400"
                                            /> Profile
                                        </DropdownItem>
                                        <DropdownItem>
                                            <FontAwesomeIcon 
                                                icon={faCogs} 
                                                size="sm" color="fw" 
                                                className="mr-2 text-gray-400"
                                            /> Settings
                                        </DropdownItem>
                                        <DropdownItem divider />
                                        <DropdownItem>
                                            <FontAwesomeIcon 
                                                icon={faSignOutAlt} 
                                                size="sm" color="fw" 
                                                className="mr-2 text-gray-400"
                                            /> Logout
                                        </DropdownItem>
                                    </DropdownMenu>
                                </UncontrolledDropdown>
                            </Nav>
                        </Navbar>
                    </Container>
                </Navbar>
            </header>
        );
    }

    toggle = () => {
        this.setState({
            isOpen: !this.state.isOpen
        });
    }
}
