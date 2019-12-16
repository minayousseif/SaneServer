import React from 'react';
import PropTypes from 'prop-types';
import { Alert } from 'reactstrap';

const AlertPanel = ({children, ...props}) => {
  const alertProps    =  {...props};
  let cssClass        = 'bg-success';
  let extraCssClasses = alertProps.className ? alertProps.className : ''; 
  if (alertProps.color) {
    const color = alertProps.color.toLowerCase();
    cssClass = `bg-${color}`;
  }
  return (
    <Alert 
      {...props} 
      className={`custom-alert text-white shadow no-border-color ${cssClass} ${extraCssClasses}`}
    >
      {children}
    </Alert>
  )
};

AlertPanel.propTypes = {
  children: PropTypes.node
};

AlertPanel.defaultProps = {
  children: ''
}

export default AlertPanel;