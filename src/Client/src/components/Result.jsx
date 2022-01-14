import React, { useState } from "react";
import axios from "axios";
import handleError from "../Helpers/handleError";

export default function Result({ data }) {
  const [updatePayload, setUpdatePayload] = useState({
    id: data.id,
    status: "COMPLETED",
    requesterId: localStorage.getItem("userId"),
    positive: "",
  });
  const [showForm, setShowForm] = useState(false);
  const [errors, setErrors] = useState([]);
  const change = (e) => {
    const target = e.target;
    const value = target.value;
    const name = target.name;
    setUpdatePayload((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    setErrors([]);
    e.preventDefault();
    try {
      await axios.patch(`/result`, updatePayload);
      window.location.reload()
    } catch (error) {
      handleError(error, setErrors);
      setUpdatePayload({ id: "", status: "COMPLETED", positive: "" });
    }
  };

  return (
    <div style={{ flexDirection: "row", justifyContent: "space-evenly" }}>
      <p>
        <span>id: {data.id}</span>
        <span>bookingId: {data.bookingId}</span>
        <span>TestLocation: {data.testLocation}</span>
        <span>TestType: {data.testType}</span>
        <span>DateCreated: {new Date(data.dateCreated).toDateString()}</span>
      </p>
      {!showForm && (
        <button onClick={() => setShowForm(true)}>Update Result</button>
      )}
      {showForm && (
        <div>
          <form onSubmit={handleSubmit}>
            <label>
              Positive
              <input
                type="radio"
                value={"true"}
                name="positive"
                onChange={change}
              />
            </label>

            <label>
              Negative
              <input
                type="radio"
                value={"false"}
                name="positive"
                onChange={change}
                required
              />
            </label>
            <input type="submit" value="Submit" />
          </form>
          {errors &&
            errors.map((error, i) => {
              return <p key={`error-${i}`}>{error}</p>;
            })}
        </div>
      )}
    </div>
  );
}
