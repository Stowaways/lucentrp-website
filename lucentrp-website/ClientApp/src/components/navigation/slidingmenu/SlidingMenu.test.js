import { render } from "@testing-library/react";

import SlidingMenu from "./SlidingMenu";

describe("SlidingMenu test cases", () => {
    test("Renders a sliding menu without crashing", () => {
        render(<SlidingMenu />);
    });

    test("Renders a closed sliding menu correctly", () => {
        const { container } = render(<SlidingMenu menuIsOpen={false} />);
        expect(container.getElementsByClassName("sliding-menu-open").length).toBe(0);
    });

    test("Renders an open sliding menu correctly", () => {
        const { container } = render(<SlidingMenu menuIsOpen={true} />);
        expect(container.getElementsByClassName("sliding-menu-open").length).toBeGreaterThan(0);
    });

    test("Renders children inside of the menu", () => {
        const { container } = render(<SlidingMenu>Test content</SlidingMenu>);
        expect(container.firstChild.textContent).toContain("Test content");
    });
});
