import React from 'react';

import { Formik, Form, Field } from 'formik';
import * as Yup from "yup";

/**
 * The signup form's initial values.
 */
const initalValues = {
    username: "",
    email: "",
    password: "",
    confirmPassword: ""
}

/**
 * The signup form's validation schema.
 */
const validationSchema = Yup.object().shape({
    username: Yup.string()
        .required(`Username is required`)
        .min(process.env.REACT_APP_MIN_USERNAME_LEN, `Username must be at least ${process.env.REACT_APP_MIN_USERNAME_LEN} characters long`)
        .max(process.env.REACT_APP_MAX_USERNAME_LEN, `Username must be less than ${process.env.REACT_APP_MIN_USERNAME_LEN + 1} characters long`),
    email: Yup.string()
        .required(`Email is required`)
        .email('Invalid email address')
        .min(process.env.REACT_APP_MIN_EMAIL_LEN, `Email must be at least ${process.env.REACT_APP_MIN_EMAIL_LEN} characters long`)
        .max(process.env.REACT_APP_MAX_EMAIL_LEN, `Email must be less than ${process.env.REACT_APP_MAX_EMAIL_LEN + 1} characters long`),
    password: Yup.string()
        .required(`Password is required`)
        .min(process.env.REACT_APP_MIN_PASSWORD_LEN, `Password must be at least ${process.env.REACT_APP_MIN_PASSWORD_LEN} characters long`)
        .max(process.env.REACT_APP_MAX_PASSWORD_LEN, `Password must be less than ${process.env.REACT_APP_MAX_PASSWORD_LEN + 1} characters long`),
    confirmPassword: Yup.string()
        .required(`Confirm password is required`)
        .oneOf([Yup.ref('password'), null], "Password does not match")
});

/**
 * The function that will handle submitting the form.
 * 
 * @param {*} values The form values. 
 * @param {*} actions Actions that can be performed to the form.
 */
const submit = (values, actions) => {
    // TODO: Implement the submit action.
}

/**
 * Create a SignupForm.
 * 
 * @returns The signup form.
 */
const SignupForm = () => {
    return (
        <Formik
            initialValues={initalValues}
            validationSchema={validationSchema}
            validateOnChange={false}
            validateOnBlur={false}
            onSubmit={(values, actions) => submit(values, actions)}
        >
            {({ isSubmitting, touched, errors}) => (
                <Form id="signup-form">
                    <div>
                        <div className="form-errror">{(touched.username && errors.username) ? errors.username : <>&nbsp;</>}</div>
                        <label htmlFor="username">Username</label>
                        <Field className="form-text-input" type="text" name="username" />
                    </div>
                    <div>
                        <div className="form-errror">{(touched.email && errors.email) ? errors.email : <>&nbsp;</>}</div>
                        <label htmlFor="email">Email</label>
                        <Field className="form-text-input" type="email" name="email" />
                    </div>
                    <div>
                        <div className="form-errror">{(touched.password && errors.password) ? errors.password : <>&nbsp;</>}</div>
                        <label htmlFor="password">Password</label>
                        <Field className="form-text-input" type="password" name="password" />
                    </div>
                    <div>
                        <div className="form-errror">{(touched.confirmPassword && errors.confirmPassword) ? errors.confirmPassword : <>&nbsp;</>}</div>
                        <label htmlFor="confirmPassword">Confirm Password</label>
                        <Field className="form-text-input" type="password" name="confirmPassword" />
                    </div>
                    <div className="form-button-container">
                        <button className="form-button" type="submit" disabled={isSubmitting}>Signup</button>
                        <button className="form-button" type="reset" disabled={isSubmitting}>Reset</button>
                    </div>
                </Form>
            )}
        </Formik>
    );
}

export default SignupForm;