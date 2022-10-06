import React from "react";
import FullPageSection from "../../layout/fullpagesection/FullPageSection";

import PageNavigationBar from "../PageNavigationBar";

import './landingpage.css';

/**
 * Create the landing page.
 * 
 * @returns The landing page.
 */
const LandingPage = () => {
    return (
        <React.Fragment>
            <PageNavigationBar />
            <FullPageSection id="hero" subtractNav={true}>
                <h1>Welcome to LucentRP</h1>
                <p>Founded on Whiskey and Cheap Perfume</p>
            </FullPageSection>
        </React.Fragment>
    );
}

export default LandingPage;