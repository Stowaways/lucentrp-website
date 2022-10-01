import "./loadingpage.css"

/**
 * Create a LoadingPage.
 * 
 * @returns The loading page.
 */
const LoadingPage = () => {
    return (
        <div className="overlay center loading-screen">
            <img className="medium-icon" src="icons/loading.svg" alt="Loading indicator" />
        </div>
    );
}

export default LoadingPage;