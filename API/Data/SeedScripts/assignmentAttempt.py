import json
import requests

with open('Attempt.json', 'r') as f:
    json_data = json.load(f)
for assignment in json_data:
    st = 1
    a = assignment["assignmentId"]
    for attempt in assignment['attempts']:
        url = 'https://localhost:7153/api/Student/Courses/1/Materials/'+str(a)+'?id='+str(st)
        # url = 'https://unipass-dev.azurewebsites.net/api/Student/Courses/1/Materials/'+str(a)+'?id='+str(st)
        payload = {'takeQuestions': attempt['takeQuestions']}
        response = requests.post(url, json=payload, verify=False)
        print(payload)
        print(response.status_code)
        st+=1
    a+=1
