import React,{useState} from "react";
import axios from "axios";
import HandleError from "../Helpers/handleError";

export default function Booking({ data }) {
    const [errors, setErrors] = useState([]);

  const handleClick = async (e) => {
    try {
      e.preventDefault();
      const cancelBody = {
        userId: localStorage.getItem("userId"),
        id:data.id,
        status: "CANCELLED",
      };
      await axios.patch(`/booking`, cancelBody);
      window.location.reload();
   
    } catch (error) {
      HandleError(error, setErrors);
    }
  };
  return (
    <div style={{ flexDirection: "row", justifyContent: "space-evenly" }}>
      <p>
        <span>
          <b>BookingId</b> : {data.id}
        </span>

        <span>
          <b>Location </b>: {data.locationName}
        </span>

        <span>
          <b>TestType</b> : {data.testType}
        </span>

        <span>
          <b>Test Date</b> : {new Date(data.dateCreated).toDateString()}
        </span>

        <button onClick={handleClick}>Cancel Booking</button>
      </p>
      <div>
      {errors &&
        errors.map((error, i) => {
          return (
            <p style={{ marginTop: "5px" }} key={`error-${i}`}>
              {error}
            </p>
          );
        })}
        </div>
      
    </div>
  );
}
