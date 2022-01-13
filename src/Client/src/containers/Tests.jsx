import React from "react";
import Test from "../components/Test";
import { useHistory } from "react-router";

export default function Bookings({ data, role }) {
  const history = useHistory();
  const handleClick = (path) => {
    history.push("/results");
  };
  return (
    <div>
      <h3>Tests</h3>
      {data &&
        data.map((test) => {
          return <Test key={test.id} data={test} />;
        })}
      {data.length < 1 && <p>You have no test result</p>}
      {role === "LABADMIN" && (
        <button onClick={handleClick}>Go To Results</button>
      )}
    </div>
  );
}
