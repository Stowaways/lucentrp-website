import { fireEvent, render } from "@testing-library/react";

import HamburgerMenu from "./HamburgerMenu";

const DemoMenu = ({ menuIsOpen, setMenuIsOpen, children }) => {
    return (
        <div className={"demo-menu" + (menuIsOpen ? " open" : " closed")}>
            {children}
        </div>
    )
}

describe("HamburgerMenu test cases", () => {
    test("Renders a hamburger menu without crashing", () => {
        render(<HamburgerMenu Content={DemoMenu} />);
    });

    test("Renders child components correctly", () => {
        const { container } = render(
            <HamburgerMenu Content={DemoMenu}>
                <p>Test child</p>
            </HamburgerMenu>
        );

        expect(container.getElementsByClassName("demo-menu").length).toBeGreaterThan(0);
        expect(container.getElementsByClassName("demo-menu")[0].textContent).toContain("Test child");
    });

    test("Opening a menu works", () => {
        const { container } = render(
            <HamburgerMenu Content={DemoMenu}>
                <p>Test child</p>
            </HamburgerMenu>
        );

        const MenuButton = container.getElementsByClassName("hamburger-menu")[0];

        expect(container.getElementsByClassName("open").length).toBe(0);
        expect(container.getElementsByClassName("closed").length).toBeGreaterThan(0);

        fireEvent.click(MenuButton);

        expect(container.getElementsByClassName("open").length).toBeGreaterThan(0);
        expect(container.getElementsByClassName("closed").length).toBe(0);
    });
});