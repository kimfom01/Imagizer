import { useIsMutating } from "@tanstack/react-query";
import { ApiResponse } from "../models";

export const ShowDownloadLink = ({ downloadUrl }: ApiResponse) => {
  const isMutating = useIsMutating({ mutationKey: ["upload"] });

  if (isMutating !== 0) {
    return <>Processing...</>;
  }

  return (
    <>
      {downloadUrl && (
        <div>
          Download Link:{" "}
          <a
            href={downloadUrl}
            target="__blank"
            rel="noreferrer"
            className="hover:underline visited:text-purple-500"
          >
            {downloadUrl}
          </a>
        </div>
      )}
    </>
  );
};
