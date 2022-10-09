import React from "react";

import './divider.css'

/**
 * Create a divider.
 * 
 * This will create a divider using flexbox by default, to use grid instead pass true 
 * in for the grid property.
 * 
 * @param {object} props The properties that will be used when rendering the divider.
 * @returns The divider.
 */
const Divider = (props) => {
    return (
        <div id={props.id ? props.id : ""} className={"divider" + (props.grid ? "-grid" : "-flex") + (props.className ? ` ${props.className}` : "")}>
            {props.children}
        </div>
    );
}

export default Divider;
