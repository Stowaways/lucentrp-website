import React from "react";

import PageNavigationBar from "../../shared/pagenavigationbar/PageNavigationBar";
import SplitFullPageSection from "../../layout/splitfullpagesection/SplitFullPageSection";
import LoginForm from "./loginform/LoginForm";

import "./loginpage.css";
import "../form.css";

/**
 * Create a login page.
 * 
 * @returns The login page.
 */
const LoginPage = () => {
    return (
        <React.Fragment>
            <PageNavigationBar />
            <SplitFullPageSection id="login" className="split-form" subtractNav={true}>
                <h1 side="left">Welcome back!</h1>
                <LoginForm side="right"/>
            </SplitFullPageSection>
        </React.Fragment>
    );
}

export default LoginPage;
