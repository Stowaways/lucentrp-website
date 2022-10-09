import { render } from "@testing-library/react";

import FullPageSection from "./FullPageSection";

describe("FullPageSection test cases", () => {
    test("Renders a full page section without crashing", () => {
        render(<FullPageSection />);
    });

    test("Renders a full page section without subtracting the navigation height", () => {
        const { container } = render(<FullPageSection />);
        expect(container.getElementsByClassName("full-page-section").length).toBeGreaterThan(0);
    });

    test("Renders a full page section with subtracting the navigation height", () => {
        const { container } = render(<FullPageSection subtractNav={true} />);
        expect(container.getElementsByClassName("full-page-section-sub-nav").length).toBeGreaterThan(0);
    });

    test("Content inside of the section gets rendered", () => {
        const { container } = render(
            <FullPageSection>
                <h1>Test text</h1>
            </FullPageSection>
        );

        expect(container.firstChild.textContent).toContain("Test text");
    });
});
