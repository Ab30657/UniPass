import requests
import json

# Read the JSON file
with open('AssignmentData.json', 'r') as f:
    data = json.load(f)

# Iterate over each assignment and send a POST request
url = 'https://localhost:7153/api/Instructor/Courses/1/Materials'
for assignment in data:
    # Set the POST request URL

    # Set the headers for the POST request
    headers = {
        'Content-Type': 'application/json'
    }

    # Set the JSON payload for the POST request
    payload = json.dumps(assignment)
    print(payload)
    # print(payload+'\n')
    # Send the POST request
    response = requests.post(url, headers=headers, data=payload, verify=False)

    # Print the response status code and content
    print(response.status_code)
    print(response.content)
