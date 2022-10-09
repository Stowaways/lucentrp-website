import { render } from '@testing-library/react';

import SignupForm from './SignupForm';

describe("Signup Form test cases", () => {
    test("Signup form renders without crashing", () => {
        render(<SignupForm />);
    });
});
