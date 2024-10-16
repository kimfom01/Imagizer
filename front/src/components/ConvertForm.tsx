import { useMutation } from "@tanstack/react-query";
import axios from "axios";
import { useState, FormEvent } from "react";
import { ApiResponse } from "../models";
import { VITE_API_URL } from "../utils/apiUtils";

interface FormProps {
  ImageFile: File | null;
  Format: string;
}

interface ConvertFormProps {
  setDownloadUrl: React.Dispatch<React.SetStateAction<string>>;
}

interface SupportedFormats {
  name: string;
  value: number;
}

const formats: SupportedFormats[] = [
  {
    name: "Gif",
    value: 81,
  },
  {
    name: "Heic",
    value: 89,
  },
  {
    name: "Jpeg",
    value: 113,
  },
  {
    name: "Jpg",
    value: 114,
  },
  {
    name: "Png",
    value: 185,
  },
  {
    name: "Raw",
    value: 207,
  },
  {
    name: "Tiff",
    value: 241,
  },
];

export const ConvertForm = ({ setDownloadUrl }: ConvertFormProps) => {
  const [form, setForm] = useState<FormProps>({
    ImageFile: null,
    Format: "112",
  });

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const formData = new FormData();
    formData.append("Format", form.Format);
    if (form.ImageFile) {
      formData.append("ImageFile", form.ImageFile);
    }

    const response = await axios.post<ApiResponse>(
      `${VITE_API_URL}/convert`,
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
    mutationKey: ["convertUpload"],
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
          <div className="flex gap-4">
            <select
              onChange={(event) =>
                setForm({ ...form, Format: event.target.value })
              }
              required
              id="size"
              className="border w-full p-2 rounded border-solid dark:bg-slate-700 border-slate-500"
            >
              <option value={""}>-- Select new format --</option>
              {formats.map((x) => {
                return (
                  <option value={x.value} key={x.value}>
                    {x.name}
                  </option>
                );
              })}
            </select>
            <button
              type="submit"
              className="border active:border active:border-white rounded border-solid border-slate-500 p-2"
            >
              Convert
            </button>
          </div>
        </div>
      </div>
    </form>
  );
};
