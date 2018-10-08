package net.gogame.gowrap.ui.v2017_2;

import android.content.Context;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.os.Parcelable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Locale;
import net.gogame.gowrap.C1426R;
import net.gogame.gowrap.inbox.DefaultMessageStateManager;
import net.gogame.gowrap.inbox.MessageState;
import net.gogame.gowrap.inbox.MessageStateManager;
import net.gogame.gowrap.model.news.Article;
import net.gogame.gowrap.model.news.Article.Category;
import net.gogame.gowrap.ui.utils.DisplayUtils;

public class NewsListAdapter extends BaseAdapter {
    private static final String KEY_ELEMENTS = "elements";
    private static final String KEY_STATE_MAP = "stateMap";
    private static final String MESSAGE_TYPE = "news";
    private static final String[] NEWS_TYPES = new String[]{"Admin", "Event", "Important", "Notice", "Summon", "Tips"};
    private final Context context;
    private final SimpleDateFormat dateFormat = new SimpleDateFormat("d/M", Locale.getDefault());
    private ArrayList<Article> elements;
    private final MessageStateManager messageStateManager;
    private HashMap<Long, MessageState> messageStateMap;

    public NewsListAdapter(Context context) {
        this.context = context;
        this.messageStateManager = new DefaultMessageStateManager(context);
    }

    public List<Article> getElements() {
        return this.elements;
    }

    public void setElements(List<Article> list) {
        if (list == null) {
            this.elements = null;
        } else if (list instanceof ArrayList) {
            this.elements = (ArrayList) list;
        } else {
            this.elements = new ArrayList(list);
        }
        List<MessageState> messageStates = this.messageStateManager.getMessageStates(MESSAGE_TYPE, System.currentTimeMillis() - 2592000000L);
        HashMap hashMap = new HashMap();
        if (messageStates != null) {
            for (MessageState messageState : messageStates) {
                if (messageState != null) {
                    hashMap.put(Long.valueOf(messageState.getId()), messageState);
                }
            }
        }
        this.messageStateMap = hashMap;
        notifyDataSetChanged();
    }

    public void markAsRead(Article article) {
        if (article.getDateTime() != null) {
            this.messageStateManager.setMessageState(MESSAGE_TYPE, article.getId(), article.getDateTime().longValue(), true);
            MessageState messageState = (MessageState) this.messageStateMap.get(Long.valueOf(article.getId()));
            if (messageState == null) {
                messageState = new MessageState();
                messageState.setType(MESSAGE_TYPE);
                messageState.setId(article.getId());
                messageState.setTimestamp(article.getDateTime().longValue());
                this.messageStateMap.put(Long.valueOf(messageState.getId()), messageState);
            }
            messageState.setRead(true);
            notifyDataSetChanged();
        }
    }

    public int getCount() {
        if (this.elements == null) {
            return 0;
        }
        return this.elements.size();
    }

    public Object getItem(int i) {
        if (i >= this.elements.size()) {
            return null;
        }
        return this.elements.get(i);
    }

    public long getItemId(int i) {
        if (i >= this.elements.size()) {
            return -1;
        }
        return ((Article) this.elements.get(i)).getId();
    }

    public View getView(int i, View view, ViewGroup viewGroup) {
        return getView(i, (Article) this.elements.get(i), view, viewGroup);
    }

    private int getLevel(Category category) {
        if (category == null) {
            return 3;
        }
        switch (category) {
            case ADMIN:
                return 0;
            case EVENT:
                return 1;
            case IMPORTANT:
                return 2;
            case NOTICE:
                return 3;
            case SUMMON:
                return 4;
            case TIPS:
                return 5;
            default:
                return 3;
        }
    }

    private void setSourceLevel(int i, ImageView... imageViewArr) {
        for (ImageView drawable : imageViewArr) {
            DisplayUtils.setLevel(drawable.getDrawable(), i);
        }
    }

    private void setBackgroundLevel(int i, View... viewArr) {
        for (View background : viewArr) {
            DisplayUtils.setLevel(background.getBackground(), i);
        }
    }

    public View getView(int i, Article article, View view, ViewGroup viewGroup) {
        if (view == null) {
            view = ((LayoutInflater) this.context.getSystemService("layout_inflater")).inflate(C1426R.layout.net_gogame_gowrap_news_list_item, viewGroup, false);
        }
        TextView textView = (TextView) view.findViewById(C1426R.id.net_gogame_gowrap_news_icon_top);
        TextView textView2 = (TextView) view.findViewById(C1426R.id.net_gogame_gowrap_news_icon_bottom);
        TextView textView3 = (TextView) view.findViewById(C1426R.id.net_gogame_gowrap_news_title);
        ImageView imageView = (ImageView) view.findViewById(C1426R.id.net_gogame_gowrap_news_status);
        if (VERSION.SDK_INT >= 17) {
            textView.setTextAlignment(4);
            textView2.setTextAlignment(4);
        }
        textView.setGravity(17);
        textView2.setGravity(17);
        if (article != null) {
            setBackgroundLevel(getLevel(article.getCategory()), textView, textView2);
            if (article.getDateTime() != null) {
                textView.setText(this.dateFormat.format(new Date(article.getDateTime().longValue())));
            }
            if (article.getCategory() != null) {
                textView2.setText(NEWS_TYPES[article.getCategory().ordinal()]);
            } else {
                textView2.setText(NEWS_TYPES[Category.NOTICE.ordinal()]);
            }
            textView3.setText(article.getTitle());
            MessageState messageState = null;
            if (this.messageStateMap != null) {
                messageState = (MessageState) this.messageStateMap.get(Long.valueOf(article.getId()));
            }
            int i2 = (messageState == null || !messageState.isRead()) ? 1 : 0;
            setSourceLevel(i2, imageView);
        } else {
            setBackgroundLevel(getLevel(null), textView, textView2);
            textView3.setText(null);
            setSourceLevel(0, imageView);
        }
        return view;
    }

    public Parcelable onSaveInstanceState() {
        Parcelable bundle = new Bundle();
        bundle.putSerializable(KEY_ELEMENTS, this.elements);
        bundle.putSerializable(KEY_STATE_MAP, this.messageStateMap);
        return bundle;
    }

    public void onRestoreInstanceState(Parcelable parcelable) {
        if (parcelable != null && (parcelable instanceof Bundle)) {
            Bundle bundle = (Bundle) parcelable;
            this.elements = (ArrayList) bundle.getSerializable(KEY_ELEMENTS);
            this.messageStateMap = (HashMap) bundle.getSerializable(KEY_STATE_MAP);
        }
    }
}
