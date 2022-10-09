import { render } from '@testing-library/react';

import DashboardPage from './DashboardPage';

describe("Dashboard page test cases", () => {
    test("Dashboard page renders without crashing", () => {
        render(<DashboardPage />);
    });
});
