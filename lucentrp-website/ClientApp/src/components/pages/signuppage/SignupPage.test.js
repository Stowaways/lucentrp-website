import { render } from '@testing-library/react';

import SignupPage from './SignupPage';

describe("Signup page test cases", () => {
    test("Signup page renders without crashing", () => {
        render(<SignupPage />);
    });
});
