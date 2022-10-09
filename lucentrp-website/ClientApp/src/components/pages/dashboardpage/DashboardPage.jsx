import React from "react";

import FullPageSection from "../../layout/fullpagesection/FullPageSection";
import PageNavigationBar from "../PageNavigationBar";

import "./dashboardpage.css";

/**
 * Create the dashboard page.
 * 
 * @returns The dashboard page.
 */
const DashboardPage = () => {
    return (
        <React.Fragment>
            <PageNavigationBar />
            <FullPageSection subtractNav={true}>

            </FullPageSection>
        </React.Fragment>
    );
}

export default DashboardPage;