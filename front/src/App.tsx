import { useIsMutating, useMutation } from "@tanstack/react-query";
import { Header } from "./components/Header";
import { VITE_API_URL } from "./utils/apiUtils";
import { FormEvent, useState } from "react";

export const App = () => {
  const [downloadUrl, setDownloadUrl] = useState("");

  return (
    <div className="container flex h-screen dark:text-white flex-col mx-auto pt-4">
      <Header />
      <div className="flex-1">
        <div className="flex flex-col gap-10 min-h-full justify-center items-center">
          <ShowDownloadLink downloadUrl={downloadUrl} />
          <ResizeForm setDownloadUrl={setDownloadUrl} />
        </div>
      </div>
      <Footer />
    </div>
  );
};

interface DownloadProp {
  downloadUrl: string;
}

const ShowDownloadLink = ({ downloadUrl }: DownloadProp) => {
  const isMutating = useIsMutating({ mutationKey: ["upload"] });

  if (isMutating !== 0) {
    return <>Loading...</>;
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

const Footer = () => {
  return (
    <div className="flex justify-center items-center mb-4">
      <footer>
        <div className="flex flex-col items-center gap-4">
          <p>&copy; Kim Fom - {new Date().getFullYear()}</p>
          <a
            href="http://localhost:5115/swagger/"
            target="_blank"
            rel="noreferrer"
            className="hover:underline visited:text-purple-500"
          >
            Are you a developer? See the Swagger Docs.
          </a>
        </div>
      </footer>
    </div>
  );
};

interface FormProps {
  Size: string;
  ImageFile: File | null;
}
interface ResizeFormProps {
  setDownloadUrl: React.Dispatch<React.SetStateAction<string>>;
}

export const ResizeForm = ({ setDownloadUrl }: ResizeFormProps) => {
  const [form, setForm] = useState<FormProps>({ Size: "", ImageFile: null });

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const formData = new FormData();
    formData.append("Size", form.Size);
    if (form.ImageFile) {
      formData.append("ImageFile", form.ImageFile);
    }

    const response = await fetch(`${VITE_API_URL}/resize`, {
      body: formData,
      method: "post",
    });

    const { downloadUrl } = await response.json();

    return downloadUrl;
  };

  const { mutateAsync } = useMutation({
    mutationFn: handleSubmit,
    onSuccess: (data) => {
      setDownloadUrl(data);
    },
    mutationKey: ["upload"],
  });

  return (
    <form onSubmit={mutateAsync}>
      <div className="flex flex-col gap-10">
        <div className="flex flex-col items-center md:flex-row justify-between mb-4 gap-4">
          <label htmlFor="size">Enter the new size:</label>
          <input
            id="size"
            name="Size"
            required
            type="number"
            onChange={(event) => setForm({ ...form, Size: event.target.value })}
            className="border rounded border-solid dark:bg-slate-700 border-slate-500"
          />
        </div>
        <div className="flex">
          <input
            name="ImageFile"
            required
            type="file"
            onChange={(event) =>
              setForm({
                ...form,
                ImageFile: Array.from(event.target.files || [])[0],
              })
            }
            className="block w-full text-sm text-slate-500 file:mr-4 file:py-2 file:px-4 file:rounded-full file:border-0 file:text-sm file:font-semibold file:bg-violet-50 file:text-violet-700 hover:file:bg-violet-100 active:file:border-white active:file:border"
          />
          <button
            type="submit"
            className="border active:border active:border-white rounded border-solid border-slate-500 p-2"
          >
            Upload
          </button>
        </div>
      </div>
    </form>
  );
};
