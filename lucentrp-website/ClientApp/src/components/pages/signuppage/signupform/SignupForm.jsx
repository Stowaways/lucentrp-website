import React, {useState} from 'react';

import { Formik, Form, Field } from 'formik';
import * as Yup from "yup";

import ErrorBox from '../../../shared/errorbox/ErrorBox';
import ErrorIcon from '../../../shared/icons/ErrorIcon';

import * as UserAccountServices from '../../../../services/userAccountServices';

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

const pascalCaseToCammelCase = (str) => {
    return str[0].toLowerCase() + str.substring(1, str.length);
}

/**
 * The function that will handle submitting the form.
 * 
 * @param {*} values The form values. 
 * @param {*} actions Actions that can be performed to the form.
 * @param {object} setServerError A function that can be used to update the
 * server error state.
 */
const submit = async (values, formikHelpers, setServerError) => {
    const signupResponse = await UserAccountServices.create(values);
    const signupBody = await signupResponse.json();

    // If server-side validation detected an error.
    if (signupResponse.status === 422) {
        for (const element of signupBody)
            formikHelpers.setFieldError(pascalCaseToCammelCase(element.propertyName), element.errorMessage);
        
        formikHelpers.setSubmitting(false);
        return;
    }

    // If anything other than success occurred.
    if (signupResponse.status !== 200) {
        setServerError({
            occurred: true,
            message: "An internal server error occurred, please try again later!"
        });

        formikHelpers.setSubmitting(false);
        return;
    }

    // The account creation has succeeded, now we can login.
    const loginResponse = await UserAccountServices.login(values);

    // If an error occurred while logging in.
    if (loginResponse.status !== 200) {
        setServerError({
            occurred: true,
            message: "Your account was created but an error occurred while logging in!"
        });

        formikHelpers.resetForm();
        formikHelpers.setSubmitting(false);
        return;
    }

    // If the user logged in successfully, redirect them to the dashboard.
    window.location.href = "dashboard";
}

/**
 * Create a SignupForm.
 * 
 * @returns The signup form.
 */
const SignupForm = () => {
    const [serverError, setServerError] = useState({
        occurred: false,
        message: ""
    })

    return (
        <React.Fragment>
            <Formik
                initialValues={initalValues}
                validationSchema={validationSchema}
                validateOnChange={false}
                validateOnBlur={false}
                onSubmit={(values, formikHelpers) => submit(values, formikHelpers, setServerError)}
            >
                {({ isSubmitting, touched, errors }) => (
                    <Form id="signup-form">
                        {serverError.occurred &&
                            <ErrorBox>
                                <ErrorIcon className="small-icon" onClick={() => setServerError({ ...serverError, occurred: false })} />
                                <p>{serverError.message}</p>
                            </ErrorBox>
                        }
                        <div>
                            <div className="form-errror">{(touched.username && errors.username) ? errors.username : <>&nbsp;</>}</div>
                            <label htmlFor="username">Username</label>
                            <Field id="username" className="form-text-input" type="text" name="username" />
                        </div>
                        <div>
                            <div className="form-errror">{(touched.email && errors.email) ? errors.email : <>&nbsp;</>}</div>
                            <label htmlFor="email">Email</label>
                            <Field id="email" className="form-text-input" type="email" name="email" />
                        </div>
                        <div>
                            <div className="form-errror">{(touched.password && errors.password) ? errors.password : <>&nbsp;</>}</div>
                            <label htmlFor="password">Password</label>
                            <Field id="password" className="form-text-input" type="password" name="password" />
                        </div>
                        <div>
                            <div className="form-errror">{(touched.confirmPassword && errors.confirmPassword) ? errors.confirmPassword : <>&nbsp;</>}</div>
                            <label htmlFor="confirmPassword">Confirm Password</label>
                            <Field id="confirmPassword" className="form-text-input" type="password" name="confirmPassword" />
                        </div>
                        <div className="form-button-container">
                            <button className="form-button" type="submit" disabled={isSubmitting}>Signup</button>
                            <button className="form-button" type="reset" disabled={isSubmitting}>Reset</button>
                        </div>
                    </Form>
                )}
            </Formik>
        </React.Fragment>
    );
}

export default SignupForm;
