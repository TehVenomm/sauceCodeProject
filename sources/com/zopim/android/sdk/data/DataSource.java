package com.zopim.android.sdk.data;

import com.zopim.android.sdk.model.Account;
import com.zopim.android.sdk.model.Agent;
import com.zopim.android.sdk.model.ChatLog;
import com.zopim.android.sdk.model.Connection;
import com.zopim.android.sdk.model.Department;
import com.zopim.android.sdk.model.Forms;
import com.zopim.android.sdk.model.Profile;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.Observer;

public interface DataSource {
    void addAccountObserver(Observer observer);

    void addAgentsObserver(Observer observer);

    void addChatLogObserver(Observer observer);

    void addConnectionObserver(Observer observer);

    void addDepartmentsObserver(Observer observer);

    void addFormsObserver(Observer observer);

    void addProfileObserver(Observer observer);

    void clear();

    void deleteAccountObserver(Observer observer);

    void deleteAgentsObserver(Observer observer);

    void deleteChatLogObserver(Observer observer);

    void deleteConnectionObserver(Observer observer);

    void deleteDepartmentsObserver(Observer observer);

    void deleteFormsObserver(Observer observer);

    void deleteObservers();

    void deleteProfileObserver(Observer observer);

    Account getAccount();

    LinkedHashMap<String, Agent> getAgents();

    LinkedHashMap<String, ChatLog> getChatLog();

    Connection getConnection();

    Map<String, Department> getDepartments();

    Forms getForms();

    Profile getProfile();
}
