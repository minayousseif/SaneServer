import React from 'react';
import { connect } from 'react-redux';
import { UncontrolledDropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEllipsisV } from '@fortawesome/free-solid-svg-icons';

import './Home.scss';

const Home = () => (
  <div className="container-fluid">
    <div className="row">
      <div className="col-sm-12 col-md-12 col-lg-12">
        <div className="card card-scanner shadow mb-4">
          <div className="card-header d-flex flex-row align-items-center justify-content-between">
            <h6 className="m-0 font-weight-bold text-primary">Scanner Perview</h6>
            <UncontrolledDropdown>
              <DropdownToggle color="link">
                <FontAwesomeIcon icon={faEllipsisV} size="sm" className="text-gray-400" color="fw" />
              </DropdownToggle>
              <DropdownMenu className="shadow animated--fade-in" right={true}>
                <DropdownItem header>Header</DropdownItem>
                <DropdownItem disabled>Action</DropdownItem>
                <DropdownItem>Another Action</DropdownItem>
                <DropdownItem divider />
                <DropdownItem>Another Action</DropdownItem>
              </DropdownMenu>
            </UncontrolledDropdown>
          </div>
          <div className="card-body">
          </div>
        </div>
      </div>
    </div>
  </div>
);

export default connect()(Home);