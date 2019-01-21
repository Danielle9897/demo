import * as React from "react";
import { Demo } from "../Demo";
import { ResultText } from "../../demoDisplay/results/resultItems";

const resultsCreator = () => <ResultText />;

export const QueryRelatedDocumentsDemo = () => <Demo   
    resultsComponents = {resultsCreator}
/>;
