// import React from 'react'

import {useForm} from "react-hook-form";
import axios from "axios";
import {ErrorMessage} from "@hookform/error-message";
import {useState} from "react";

const {VITE_API_URL} = import.meta.env
export default function CreateChargePage() {
    const { register, setValue, control, handleSubmit, getValues, watch, reset, trigger, formState: { errors } } = useForm();
    const [authErrors, setAuthErrors] = useState<string[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const chargeSubmit = (data:any)=> {
        console.log(import.meta.env)
        console.log(VITE_API_URL)
        setLoading(true);
        setAuthErrors([]);
        const bodyFormData = new FormData();
        for ( const key in data ) {
            bodyFormData.append(key, data[key]);
        }
        axios({
            method: "post",
            url: VITE_API_URL+'/charges',
            data: bodyFormData,
            headers: { "Content-Type": "multipart/form-data" },
        }).then((res:any) => {
            if (res.status==200) {
                console.log(res);
            }else { }
        }).catch((err:any) => {
            console.log(err)
            if (err.response.data.errors){
                setAuthErrors(err.response.data.errors);
            }
        }).finally(() => {
            setLoading(false);
        });
    };
    return (
        <div  className={"container mt-5 pt-5"}>
            <form className="form mw-500px" method="post"
                  onSubmit={handleSubmit(chargeSubmit)}>
                <div className=" my-3">
                    <input className="form-control bg-transparent" aria-required="true"
                           placeholder="Firstname" {...register("FirstName", {
                        required: {value: true, message: "This is required."}, onChange(e: any) {
                        }
                    })} />
                    <div className="text-danger"><ErrorMessage errors={errors} name="FirstName"/></div>
                </div>
                <div className=" my-3">
                    <input className="form-control bg-transparent" aria-required="true"
                           placeholder="LastName" {...register("LastName", {
                        required: {value: true, message: "This is required."}, onChange(e: any) {
                        }
                    })} />
                    <div className="text-danger"><ErrorMessage errors={errors} name="LastName"/></div>
                </div>
                <div className=" my-3">
                    <input className="form-control bg-transparent" aria-required="true"
                           placeholder="Email" {...register("Email", {
                        required: {value: true, message: "This is required."}, onChange(e: any) {
                        }
                    })} />
                    <div className="text-danger"><ErrorMessage errors={errors} name="Email"/></div>
                </div>
                <div className=" my-3">
                    <input className="form-control bg-transparent" aria-required="true"
                           placeholder="Address" {...register("Address", {
                        required: {value: true, message: "This is required."}, onChange(e: any) {
                        }
                    })} />
                    <div className="text-danger"><ErrorMessage errors={errors} name="Address"/></div>
                </div>
                <div className=" my-3">
                    <input className="form-control bg-transparent" aria-required="true"
                           placeholder="PostalCode" {...register("PostalCode", {
                        required: {value: true, message: "This is required."}, onChange(e: any) {
                        }
                    })} />
                    <div className="text-danger"><ErrorMessage errors={errors} name="PostalCode"/></div>
                </div>
                <div className=" my-3">
                    <textarea className="form-control bg-transparent" aria-required="true"
                           placeholder="Description" {...register("Description", {
                        required: {value: true, message: "This is required."}, onChange(e: any) {
                        }
                    })} />
                    <div className="text-danger"><ErrorMessage errors={errors} name="Description"/></div>
                </div>
                <div className=" my-3">
                    <input className="form-control bg-transparent" aria-required="true"
                           placeholder="Amount" {...register("Amount", {
                        required: {value: true, message: "This is required."}, onChange(e: any) {
                        }
                    })} />
                    <div className="text-danger"><ErrorMessage errors={errors} name="Amount"/></div>
                </div>
                <div className="d-grid mb-10 mt-3">
                    <button type="submit" className="btn btn-primary">
                        <span className="indicator-label">Create Charge</span>
                    </button>
                </div>
            </form>
        </div>
    )
}
