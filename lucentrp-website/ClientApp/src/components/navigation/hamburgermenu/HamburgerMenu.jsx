import React, { useState } from "react";

import './hamburgermenu.css';

/**
 * Create a HamburgerMenu.
 * 
 * A Content property should be passed in containing a Component that accepts two
 * properties: menuIsOpen and setMenuIsOpen.
 * 
 * @param {*} props Properties that will be used to render the hamburger menu.
 * @returns The hamburger menu.
 */
const HamburgerMenu = ({ id, className, Content, children }) => {
    const [menuIsOpen, setMenuIsOpen] = useState(false);

    return (
        <React.Fragment>
            <div
                id={id ? id : ""}
                className={"hamburger-menu" + (className ? ` ${className}` : "")}
                onClick={() => setMenuIsOpen(true)}
            >
                <span className="line line1" />
                <span className="line line2" />
                <span className="line line3" />
            </div>
            <div className={menuIsOpen ? "overlay" : ""} onClick={() => setMenuIsOpen(false)} />
            <Content menuIsOpen={menuIsOpen} setMenuIsOpen={setMenuIsOpen} children={children} />
        </React.Fragment>
    );
}

export default HamburgerMenu;