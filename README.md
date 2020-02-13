# Sitecore GTM Connector

This module is based on the original repository and presentation at Sitecore Symposium 2019.  The repository (https://github.com/davidruckman/SitecoreGtmIntegration) was limited and left alot to be desired.  I wanted to shift away from the POC style implementation, into more of a polished product that anyone can add to their site.

This implementation has been tested with 9.3 (however it's likely to work with all versions of 9.x+) and has a demo using Sitecore Docker that you can use to test this locally (Ensure you have run the Sitecore Docker Images repository first before running `docker-compose`.  Or you can refer to the details below on how to install the Google Tag Manager templates in your GTM instance and then just install the Sitecore package in your environment.

Currently this version supports Triggering the following actions from Google Tag Manager:

- Goals
- Outcomes

Refer to the "Issues" tab for future roadmap items.