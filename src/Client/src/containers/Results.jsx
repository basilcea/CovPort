import React, { useState, useEffect } from "react";
import "react-calendar/dist/Calendar.css";
import axios from "axios";
import HandleError from "../Helpers/handleError";
import { NavLink } from "react-router-dom";
import Result from "../components/Result";

export default function CreateResult() {
  const [result, setResult] = useState();
  const [errors, setErrors] = useState([]);
  const [createPayload, setCreatePayload] = useState({
    bookingId: "",
    userId: "",
    requesterId: localStorage.getItem("userId"),
    testType: "",
    testLocation: "",
  });

  const [getPending, setGetPending] = useState([]);

  useEffect(() => {
    const getTests = async () => {
      await getPendingTests("PENDING");
    };
    getTests();
  }, []);

  const change = (e) => {
    setErrors([]);
    const target = e.target;
    const value = target.value;
    const name = target.name;
    setCreatePayload((prev) => ({ ...prev, [name]: value }));
  };
  const getPendingTests = async (status) => {
    const {
      data: { data },
    } = await axios.get(`/result?status=${status}`);

    setGetPending((prev) => [...prev, ...data]);
  };

  const getBooking = async (e) => {
    e.preventDefault();
    try {
      const {
        data: { data },
      } = await axios.get(`/booking/${createPayload.bookingId}`);
      const { userId, testType, locationName } = data;
      setCreatePayload((prev) => ({
        ...prev,
        userId,
        testType,
        testLocation: locationName,
      }));
    } catch (error) {
      HandleError(error, setErrors);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const {
        data: { data },
      } = await axios.post(`/result`, createPayload);
      setResult(data);
    } catch (error) {
      HandleError(error, setErrors);
      setCreatePayload({
        bookingId: "",
        userId: "",
        requesterId: localStorage.getItem("userId"),
        testType: "",
        testLocation: "",
      });
    }
  };

  return (
    <div>
      <div>
        <h2> Create Results</h2>

        {!result && (
          <div>
            <form onSubmit={getBooking}>
              <input
                type="number"
                value={createPayload.bookingId}
                name="bookingId"
                placeholder="Booking Id"
                onChange={change}
                required
              />
              <input type="submit" value="Enter" />
            </form>

            {createPayload.userId && (
              <div>
                <p>Booking Id : {createPayload.bookingId}</p>
                <p>TestType: {createPayload.testType}</p>
                <p>TestLocation: {createPayload.testLocation} </p>
                <button onClick={handleSubmit}> Create Result</button>
              </div>
            )}
          </div>
        )}
        {result && (
          <div style={{ marginTop: "5px" }}>
            <b> Result Created </b>
            <NavLink to="/">View Result</NavLink>
          </div>
        )}
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
      <div>
        <h3>Pending Results</h3>
        {getPending.length > 0 &&
          getPending.map((pending) => (
            <Result data={pending} key={`r${pending.id}`} />
          ))}
        {getPending.length < 1 && <b> No Pending Results</b>}
      </div>
    </div>
  );
}
