package net.gogame.gowrap.integrations.core;

import java.util.List;
import java.util.Map;

public class ServerStatus {
    private Map<String, LocalizedStatus> locales;
    private Status status;

    public static class LocalizedStatus {
        private List<StatusFaqEntry> faq;
        private String message;
        private String title;
        private String url;

        public String getTitle() {
            return this.title;
        }

        public void setTitle(String str) {
            this.title = str;
        }

        public String getMessage() {
            return this.message;
        }

        public void setMessage(String str) {
            this.message = str;
        }

        public String getUrl() {
            return this.url;
        }

        public void setUrl(String str) {
            this.url = str;
        }

        public List<StatusFaqEntry> getFaq() {
            return this.faq;
        }

        public void setFaq(List<StatusFaqEntry> list) {
            this.faq = list;
        }
    }

    public enum Status {
        OK,
        DOWN,
        MAINTENANCE
    }

    public static class StatusFaqEntry {
        private String answer;
        private String question;

        public String getQuestion() {
            return this.question;
        }

        public void setQuestion(String str) {
            this.question = str;
        }

        public String getAnswer() {
            return this.answer;
        }

        public void setAnswer(String str) {
            this.answer = str;
        }
    }

    public Status getStatus() {
        return this.status;
    }

    public void setStatus(Status status) {
        this.status = status;
    }

    public Map<String, LocalizedStatus> getLocales() {
        return this.locales;
    }

    public void setLocales(Map<String, LocalizedStatus> map) {
        this.locales = map;
    }
}
