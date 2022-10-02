import React from "react";

import PageNavigationBar from "../PageNavigationBar";
import SplitFullPageSection from "../../layout/splitfullpagesection/SplitFullPageSection";
import SignupForm from "./signupform/SignupForm";

import "./signuppage.css";
import "../form.css";

/**
 * Create a SignupPage
 * 
 * @returns The signup page.
 */
const SignupPage = () => {
    return (
        <React.Fragment>
            <PageNavigationBar />
            <SplitFullPageSection id="signup" className="split-form" subtractNav={true}>
                <h1 side="left">Welcome to the party!</h1>
                <SignupForm side="right" />
            </SplitFullPageSection>
        </React.Fragment>
    );
}

export default SignupPage;