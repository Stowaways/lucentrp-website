import React from "react";

import PageNavigationBar from "../PageNavigationBar";
import FullPageSection from "../../layout/fullpagesection/FullPageSection";

import "./signuppage.css"
import Divider from "../../layout/divider/Divider";

import SignupForm from "./signupform/SignupForm";

/**
 * Create a SignupPage
 * 
 * @returns The signup page.
 */
const SignupPage = () => {
    return (
        <React.Fragment>
            <PageNavigationBar />
            <FullPageSection id="signup" subtractNav={true}>
                <Divider grid={true}>
                    <div className="image-box">
                        <h1>Welcome to the party!</h1>
                    </div>
                    <div className="form-wrapper">
                        <SignupForm />
                    </div>
                </Divider>
            </FullPageSection>
        </React.Fragment>
    );
}

export default SignupPage;