import { Header } from "./components/Header";
import { useState } from "react";
import { Footer } from "./components/Footer";
import { ResizeForm } from "./components/ResizeForm";
import { ShowDownloadLink } from "./components/ShowDownloadLink";
import { ConvertForm } from "./components/ConvertForm";

export const App = () => {
  const [downloadUrl, setDownloadUrl] = useState("");
  const [toggle, setToggle] = useState(false);

  const selectedStyle = "bg-slate-500 p-2 rounded-lg";

  return (
    <div className="container flex h-screen dark:text-white flex-col mx-auto p-4">
      <Header />
      <div className="flex justify-center h-full w-full">
        <div className="flex flex-col gap-10 min-h-full w-full md:w-fit justify-center items-center">
          <div className="flex justify-between w-full">
            <button
              className={`${toggle && selectedStyle}`}
              onClick={() => setToggle(true)}
            >
              Resize Image
            </button>
            <button
              className={`${!toggle && selectedStyle}`}
              onClick={() => setToggle(false)}
            >
              Convert Image
            </button>
          </div>
          <ShowDownloadLink downloadUrl={downloadUrl} />
          {toggle ? (
            <ResizeForm setDownloadUrl={setDownloadUrl} />
          ) : (
            <ConvertForm setDownloadUrl={setDownloadUrl} />
          )}
        </div>
      </div>
      <Footer />
    </div>
  );
};
