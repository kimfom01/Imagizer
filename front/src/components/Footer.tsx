import { VITE_DOCS_URL } from "../utils/apiUtils";

export const Footer = () => {
  return (
    <div className="flex justify-center items-center mb-4">
      <footer>
        <div className="flex flex-col items-center gap-4">
          <p>&copy; Kim Fom - {new Date().getFullYear()}</p>
          <a
            href={`${VITE_DOCS_URL}`}
            target="_blank"
            rel="noreferrer"
            className="hover:underline visited:text-purple-500"
          >
            Are you a developer? See the Documentation.
          </a>
        </div>
      </footer>
    </div>
  );
};
