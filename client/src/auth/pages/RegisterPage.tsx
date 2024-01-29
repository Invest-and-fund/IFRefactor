/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable no-empty */
/* eslint-disable @typescript-eslint/no-explicit-any */
import {useState} from "react";
import axios from "axios";
import {useForm} from "react-hook-form";
import {Link} from "react-router-dom";

export default function RegisterPage () {
    const { register, handleSubmit} = useForm();
    const [chargeErrors, setChargeErrors] = useState<string[]>([]);
    const [loading, setLoading] = useState<boolean>(false);

    const registerSubmit = (data:any)=> {
        setChargeErrors([]);
        setLoading(true);
        setChargeErrors([]);
        const bodyFormData = new FormData();
        for ( const key in data ) {
            bodyFormData.append(key, data[key]);
        }
        axios({
            method: "post",
            url: '/auth/register',
            data: bodyFormData,
            headers: { "Content-Type": "multipart/form-data" },
        }).then((res:any) => {
            if (res.status==200) {
                console.log(res);
            }else { }
        }).catch((err:any) => {
            console.log(err)
            if (err.response.data.errors){
                setChargeErrors(err.response.data.errors);
            }
            // if (err.response.status === 403) window.location.replace("/auth/account/login");
        }).finally(() => {
            setLoading(false);
        });
    };

    return (<>
        {
            !loading?
            <div className={"container mt-5 pt-5"}>
                <div className="text-danger my-5" role="alert">
                    {chargeErrors.map((error: any) => (
                        <div>{error}</div>
                    ))}
                </div>
                <form className="form mw-500px" method="post"
                      onSubmit={handleSubmit(registerSubmit)}>
                    <div className=" my-2">
                        <input className="form-control bg-transparent" aria-required="true"
                               placeholder="Firstname" {...register("FirstName", {
                            required: {value: true, message: "This is required."}
                        })} />
                        <span className="text-danger"></span>
                    </div>
                    <div className=" my-2">
                        <input  {...register("LastName", {
                            required: {value: true, message: "This is required."}
                        })} className="form-control bg-transparent" aria-required="true" placeholder="Lastname"/>
                        <span className="text-danger"></span>
                    </div>
                    <div className=" my-2">
                        <input className="form-control bg-transparent" aria-required="true"
                               placeholder="UserName" {...register("UserName", {required: "This is required."})} />
                    </div>
                    <div className=" my-2">
                        <input className="form-control bg-transparent" autoComplete="username" aria-required="true"
                               placeholder="name@example.com" {...register("Email", {required: "This is required."})} />
                    </div>
                    <div className="">
                        <div className="mb-1">
                            <div className="position-relative mb-3">
                                <input className="form-control bg-transparent" autoComplete="new-password"
                                       aria-required="true"
                                       placeholder="password" {...register("Password", {required: "This is required."})} />
                            </div>
                        </div>
                    </div>
                    <div className=" mb-8">
                        <input className="form-control bg-transparent" autoComplete="off" aria-required="true"
                               placeholder="password" {...register("ConfirmPassword", {required: "This is required."})} />
                    </div>
                    <div className="d-grid mb-10 mt-3">
                        <button type="submit" className="btn btn-primary">
                            <span className="indicator-label">Sign up</span>
                        </button>
                    </div>
                    <div className="text-center mt-3">
                        Already have an Account?
                        <Link className="link-primary fw-semibold ms-2" to="/auth/login">Login</Link>
                    </div>
                </form>
            </div>
            :
            <div>Loading...</div>
        }
        </>
    )
}
