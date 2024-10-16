import { useMutation } from "@tanstack/react-query";
import axios from "axios";
import { useState, FormEvent } from "react";
import { VITE_API_URL } from "../utils/apiUtils";
import { ApiResponse } from "../models";

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

    const response = await axios.post<ApiResponse>(
      `${VITE_API_URL}/resize`,
      formData,
      {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      }
    );

    const { downloadUrl } = response.data;

    return downloadUrl;
  };

  const { mutateAsync } = useMutation({
    mutationFn: handleSubmit,
    onSuccess: (data) => {
      setDownloadUrl(data);
    },
    mutationKey: ["resizeUpload"],
  });

  return (
    <form className="w-full md:w-fit" onSubmit={mutateAsync}>
      <div className="flex flex-col w-full gap-10">
        <div className="flex flex-col w-full items-center justify-between mb-4 gap-4">
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
            className="block border-2 border-slate-500 rounded-lg w-full text-sm text-slate-500 file:mr-4 file:py-2 file:px-4 file:rounded-full file:border-0 file:text-sm file:font-semibold file:bg-violet-50 file:text-violet-700 hover:file:bg-violet-100 active:file:border-white active:file:border"
          />
        </div>
        <div className="flex flex-col gap-4">
          <label className="self-center" htmlFor="size">
            Enter the new size:
          </label>
          <div className="flex gap-4">
            <input
              id="size"
              name="Size"
              required
              type="number"
              onChange={(event) =>
                setForm({ ...form, Size: event.target.value })
              }
              className="border w-full p-2 rounded border-solid dark:bg-slate-700 border-slate-500"
            />
            <button
              type="submit"
              className="border active:border active:border-white rounded border-solid border-slate-500 p-2"
            >
              Resize
            </button>
          </div>
        </div>
      </div>
    </form>
  );
};
