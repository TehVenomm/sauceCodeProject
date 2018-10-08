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

public class PathDataSource implements DataSource {
    private LivechatAccountPath mAccountPath = LivechatAccountPath.getInstance();
    private LivechatAgentsPath mAgentsPath = LivechatAgentsPath.getInstance();
    private LivechatChatLogPath mChatLogPath = LivechatChatLogPath.getInstance();
    private ConnectionPath mConnectionPath = ConnectionPath.getInstance();
    private LivechatDepartmentsPath mDepartmentsPath = LivechatDepartmentsPath.getInstance();
    private LivechatFormsPath mFormsPath = LivechatFormsPath.getInstance();
    private LivechatProfilePath mProfilePath = LivechatProfilePath.getInstance();

    public void addAccountObserver(Observer observer) {
        synchronized (this.mAccountPath) {
            this.mAccountPath.addObserver(observer);
        }
    }

    public void addAgentsObserver(Observer observer) {
        synchronized (this.mAgentsPath) {
            this.mAgentsPath.addObserver(observer);
        }
    }

    public void addChatLogObserver(Observer observer) {
        synchronized (this.mChatLogPath) {
            this.mChatLogPath.addObserver(observer);
        }
    }

    public void addConnectionObserver(Observer observer) {
        synchronized (this.mConnectionPath) {
            this.mConnectionPath.addObserver(observer);
        }
    }

    public void addDepartmentsObserver(Observer observer) {
        synchronized (this.mDepartmentsPath) {
            this.mDepartmentsPath.addObserver(observer);
        }
    }

    public void addFormsObserver(Observer observer) {
        synchronized (this.mFormsPath) {
            this.mFormsPath.addObserver(observer);
        }
    }

    public void addProfileObserver(Observer observer) {
        synchronized (this.mProfilePath) {
            this.mProfilePath.addObserver(observer);
        }
    }

    public synchronized void clear() {
        this.mConnectionPath.clear();
    }

    public void deleteAccountObserver(Observer observer) {
        synchronized (this.mAccountPath) {
            this.mAccountPath.deleteObserver(observer);
        }
    }

    public void deleteAgentsObserver(Observer observer) {
        synchronized (this.mAgentsPath) {
            this.mAgentsPath.deleteObserver(observer);
        }
    }

    public void deleteChatLogObserver(Observer observer) {
        synchronized (this.mChatLogPath) {
            this.mChatLogPath.deleteObserver(observer);
        }
    }

    public void deleteConnectionObserver(Observer observer) {
        synchronized (this.mConnectionPath) {
            this.mConnectionPath.deleteObserver(observer);
        }
    }

    public void deleteDepartmentsObserver(Observer observer) {
        synchronized (this.mDepartmentsPath) {
            this.mDepartmentsPath.deleteObserver(observer);
        }
    }

    public void deleteFormsObserver(Observer observer) {
        synchronized (this.mFormsPath) {
            this.mFormsPath.deleteObserver(observer);
        }
    }

    public synchronized void deleteObservers() {
        this.mConnectionPath.deleteObservers();
    }

    public void deleteProfileObserver(Observer observer) {
        synchronized (this.mProfilePath) {
            this.mProfilePath.deleteObserver(observer);
        }
    }

    public Account getAccount() {
        return this.mAccountPath.getData();
    }

    public LinkedHashMap<String, Agent> getAgents() {
        return this.mAgentsPath.getData();
    }

    public LinkedHashMap<String, ChatLog> getChatLog() {
        return this.mChatLogPath.getData();
    }

    public Connection getConnection() {
        return this.mConnectionPath.getData();
    }

    public Map<String, Department> getDepartments() {
        return this.mDepartmentsPath.getData();
    }

    public Forms getForms() {
        return this.mFormsPath.getData();
    }

    public Profile getProfile() {
        return this.mProfilePath.getData();
    }
}
