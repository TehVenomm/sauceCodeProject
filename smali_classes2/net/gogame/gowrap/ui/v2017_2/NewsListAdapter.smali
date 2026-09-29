.class public Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;
.super Landroid/widget/BaseAdapter;
.source "NewsListAdapter.java"


# static fields
.field private static final KEY_ELEMENTS:Ljava/lang/String; = "elements"

.field private static final KEY_STATE_MAP:Ljava/lang/String; = "stateMap"

.field private static final MESSAGE_TYPE:Ljava/lang/String; = "news"

.field private static final NEWS_TYPES:[Ljava/lang/String;


# instance fields
.field private final context:Landroid/content/Context;

.field private final dateFormat:Ljava/text/SimpleDateFormat;

.field private elements:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "Lnet/gogame/gowrap/model/news/Article;",
            ">;"
        }
    .end annotation
.end field

.field private final messageStateManager:Lnet/gogame/gowrap/inbox/MessageStateManager;

.field private messageStateMap:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/Long;",
            "Lnet/gogame/gowrap/inbox/MessageState;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 6

    const-string v0, "Admin"

    const-string v1, "Event"

    const-string v2, "Important"

    const-string v3, "Notice"

    const-string v4, "Summon"

    const-string v5, "Tips"

    .line 31
    filled-new-array/range {v0 .. v5}, [Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->NEWS_TYPES:[Ljava/lang/String;

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 3

    .line 46
    invoke-direct {p0}, Landroid/widget/BaseAdapter;-><init>()V

    .line 38
    new-instance v0, Ljava/text/SimpleDateFormat;

    const-string v1, "d/M"

    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v2

    invoke-direct {v0, v1, v2}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->dateFormat:Ljava/text/SimpleDateFormat;

    .line 48
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->context:Landroid/content/Context;

    .line 49
    new-instance v0, Lnet/gogame/gowrap/inbox/DefaultMessageStateManager;

    invoke-direct {v0, p1}, Lnet/gogame/gowrap/inbox/DefaultMessageStateManager;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->messageStateManager:Lnet/gogame/gowrap/inbox/MessageStateManager;

    return-void
.end method

.method private getLevel(Lnet/gogame/gowrap/model/news/Article$Category;)I
    .locals 2

    const/4 v0, 0x3

    if-nez p1, :cond_0

    return v0

    .line 136
    :cond_0
    sget-object v1, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter$1;->$SwitchMap$net$gogame$gowrap$model$news$Article$Category:[I

    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/Article$Category;->ordinal()I

    move-result p1

    aget p1, v1, p1

    packed-switch p1, :pswitch_data_0

    return v0

    :pswitch_0
    const/4 p1, 0x5

    return p1

    :pswitch_1
    const/4 p1, 0x4

    return p1

    :pswitch_2
    return v0

    :pswitch_3
    const/4 p1, 0x2

    return p1

    :pswitch_4
    const/4 p1, 0x1

    return p1

    :pswitch_5
    const/4 p1, 0x0

    return p1

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_5
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method private varargs setBackgroundLevel(I[Landroid/view/View;)V
    .locals 3

    .line 161
    array-length v0, p2

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v0, :cond_0

    aget-object v2, p2, v1

    .line 162
    invoke-virtual {v2}, Landroid/view/View;->getBackground()Landroid/graphics/drawable/Drawable;

    move-result-object v2

    invoke-static {v2, p1}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->setLevel(Landroid/graphics/drawable/Drawable;I)V

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_0
    return-void
.end method

.method private varargs setSourceLevel(I[Landroid/widget/ImageView;)V
    .locals 3

    .line 155
    array-length v0, p2

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v0, :cond_0

    aget-object v2, p2, v1

    .line 156
    invoke-virtual {v2}, Landroid/widget/ImageView;->getDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object v2

    invoke-static {v2, p1}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->setLevel(Landroid/graphics/drawable/Drawable;I)V

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_0
    return-void
.end method


# virtual methods
.method public getCount()I
    .locals 1

    .line 103
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->elements:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return v0

    .line 106
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->elements:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->size()I

    move-result v0

    return v0
.end method

.method public getElements()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/news/Article;",
            ">;"
        }
    .end annotation

    .line 53
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->elements:Ljava/util/ArrayList;

    return-object v0
.end method

.method public getItem(I)Ljava/lang/Object;
    .locals 1

    .line 111
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->elements:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->size()I

    move-result v0

    if-lt p1, v0, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 114
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->elements:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public getItemId(I)J
    .locals 2

    .line 119
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->elements:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->size()I

    move-result v0

    if-lt p1, v0, :cond_0

    const-wide/16 v0, -0x1

    return-wide v0

    .line 122
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->elements:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gowrap/model/news/Article;

    .line 123
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/Article;->getId()J

    move-result-wide v0

    return-wide v0
.end method

.method public getView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 1

    .line 128
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->elements:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/model/news/Article;

    .line 129
    invoke-virtual {p0, p1, v0, p2, p3}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->getView(ILnet/gogame/gowrap/model/news/Article;Landroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public getView(ILnet/gogame/gowrap/model/news/Article;Landroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 9

    const/4 p1, 0x0

    if-nez p3, :cond_0

    .line 168
    iget-object p3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->context:Landroid/content/Context;

    const-string v0, "layout_inflater"

    invoke-virtual {p3, v0}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p3

    check-cast p3, Landroid/view/LayoutInflater;

    .line 170
    sget v0, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_news_list_item:I

    invoke-virtual {p3, v0, p4, p1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p3

    .line 174
    :cond_0
    sget p4, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_news_icon_top:I

    invoke-virtual {p3, p4}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p4

    check-cast p4, Landroid/widget/TextView;

    .line 176
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_news_icon_bottom:I

    invoke-virtual {p3, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    .line 178
    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_news_title:I

    invoke-virtual {p3, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroid/widget/TextView;

    .line 180
    sget v2, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_news_status:I

    invoke-virtual {p3, v2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v2

    check-cast v2, Landroid/widget/ImageView;

    .line 183
    sget v3, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v4, 0x11

    if-lt v3, v4, :cond_1

    const/4 v3, 0x4

    .line 184
    invoke-virtual {p4, v3}, Landroid/widget/TextView;->setTextAlignment(I)V

    .line 185
    invoke-virtual {v0, v3}, Landroid/widget/TextView;->setTextAlignment(I)V

    .line 187
    :cond_1
    invoke-virtual {p4, v4}, Landroid/widget/TextView;->setGravity(I)V

    .line 188
    invoke-virtual {v0, v4}, Landroid/widget/TextView;->setGravity(I)V

    const/4 v3, 0x2

    const/4 v4, 0x0

    const/4 v5, 0x1

    if-eqz p2, :cond_7

    .line 191
    invoke-virtual {p2}, Lnet/gogame/gowrap/model/news/Article;->getCategory()Lnet/gogame/gowrap/model/news/Article$Category;

    move-result-object v6

    invoke-direct {p0, v6}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->getLevel(Lnet/gogame/gowrap/model/news/Article$Category;)I

    move-result v6

    new-array v3, v3, [Landroid/view/View;

    aput-object p4, v3, p1

    aput-object v0, v3, v5

    invoke-direct {p0, v6, v3}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->setBackgroundLevel(I[Landroid/view/View;)V

    .line 192
    invoke-virtual {p2}, Lnet/gogame/gowrap/model/news/Article;->getDateTime()Ljava/lang/Long;

    move-result-object v3

    if-eqz v3, :cond_2

    .line 193
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->dateFormat:Ljava/text/SimpleDateFormat;

    new-instance v6, Ljava/util/Date;

    invoke-virtual {p2}, Lnet/gogame/gowrap/model/news/Article;->getDateTime()Ljava/lang/Long;

    move-result-object v7

    invoke-virtual {v7}, Ljava/lang/Long;->longValue()J

    move-result-wide v7

    invoke-direct {v6, v7, v8}, Ljava/util/Date;-><init>(J)V

    invoke-virtual {v3, v6}, Ljava/text/SimpleDateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p4, v3}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 195
    :cond_2
    invoke-virtual {p2}, Lnet/gogame/gowrap/model/news/Article;->getCategory()Lnet/gogame/gowrap/model/news/Article$Category;

    move-result-object p4

    if-eqz p4, :cond_3

    .line 196
    sget-object p4, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->NEWS_TYPES:[Ljava/lang/String;

    invoke-virtual {p2}, Lnet/gogame/gowrap/model/news/Article;->getCategory()Lnet/gogame/gowrap/model/news/Article$Category;

    move-result-object v3

    invoke-virtual {v3}, Lnet/gogame/gowrap/model/news/Article$Category;->ordinal()I

    move-result v3

    aget-object p4, p4, v3

    invoke-virtual {v0, p4}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    goto :goto_0

    .line 198
    :cond_3
    sget-object p4, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->NEWS_TYPES:[Ljava/lang/String;

    sget-object v3, Lnet/gogame/gowrap/model/news/Article$Category;->NOTICE:Lnet/gogame/gowrap/model/news/Article$Category;

    invoke-virtual {v3}, Lnet/gogame/gowrap/model/news/Article$Category;->ordinal()I

    move-result v3

    aget-object p4, p4, v3

    invoke-virtual {v0, p4}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 200
    :goto_0
    invoke-virtual {p2}, Lnet/gogame/gowrap/model/news/Article;->getTitle()Ljava/lang/String;

    move-result-object p4

    invoke-virtual {v1, p4}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 203
    iget-object p4, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->messageStateMap:Ljava/util/HashMap;

    if-eqz p4, :cond_4

    .line 204
    iget-object p4, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->messageStateMap:Ljava/util/HashMap;

    invoke-virtual {p2}, Lnet/gogame/gowrap/model/news/Article;->getId()J

    move-result-wide v0

    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p2

    invoke-virtual {p4, p2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p2

    move-object v4, p2

    check-cast v4, Lnet/gogame/gowrap/inbox/MessageState;

    :cond_4
    if-eqz v4, :cond_6

    .line 206
    invoke-virtual {v4}, Lnet/gogame/gowrap/inbox/MessageState;->isRead()Z

    move-result p2

    if-nez p2, :cond_5

    goto :goto_1

    :cond_5
    const/4 p2, 0x0

    goto :goto_2

    :cond_6
    :goto_1
    const/4 p2, 0x1

    :goto_2
    new-array p4, v5, [Landroid/widget/ImageView;

    aput-object v2, p4, p1

    invoke-direct {p0, p2, p4}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->setSourceLevel(I[Landroid/widget/ImageView;)V

    goto :goto_3

    .line 208
    :cond_7
    invoke-direct {p0, v4}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->getLevel(Lnet/gogame/gowrap/model/news/Article$Category;)I

    move-result p2

    new-array v3, v3, [Landroid/view/View;

    aput-object p4, v3, p1

    aput-object v0, v3, v5

    invoke-direct {p0, p2, v3}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->setBackgroundLevel(I[Landroid/view/View;)V

    .line 209
    invoke-virtual {v1, v4}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 210
    new-array p2, v5, [Landroid/widget/ImageView;

    aput-object v2, p2, p1

    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->setSourceLevel(I[Landroid/widget/ImageView;)V

    :goto_3
    return-object p3
.end method

.method public markAsRead(Lnet/gogame/gowrap/model/news/Article;)V
    .locals 8

    .line 82
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/Article;->getDateTime()Ljava/lang/Long;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 86
    :cond_0
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->messageStateManager:Lnet/gogame/gowrap/inbox/MessageStateManager;

    const-string v2, "news"

    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/Article;->getId()J

    move-result-wide v3

    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/Article;->getDateTime()Ljava/lang/Long;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v5

    const/4 v7, 0x1

    invoke-interface/range {v1 .. v7}, Lnet/gogame/gowrap/inbox/MessageStateManager;->setMessageState(Ljava/lang/String;JJZ)V

    .line 89
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->messageStateMap:Ljava/util/HashMap;

    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/Article;->getId()J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/inbox/MessageState;

    if-nez v0, :cond_1

    .line 91
    new-instance v0, Lnet/gogame/gowrap/inbox/MessageState;

    invoke-direct {v0}, Lnet/gogame/gowrap/inbox/MessageState;-><init>()V

    const-string v1, "news"

    .line 92
    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/inbox/MessageState;->setType(Ljava/lang/String;)V

    .line 93
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/Article;->getId()J

    move-result-wide v1

    invoke-virtual {v0, v1, v2}, Lnet/gogame/gowrap/inbox/MessageState;->setId(J)V

    .line 94
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/news/Article;->getDateTime()Ljava/lang/Long;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-virtual {v0, v1, v2}, Lnet/gogame/gowrap/inbox/MessageState;->setTimestamp(J)V

    .line 95
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->messageStateMap:Ljava/util/HashMap;

    invoke-virtual {v0}, Lnet/gogame/gowrap/inbox/MessageState;->getId()J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {p1, v1, v0}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    const/4 p1, 0x1

    .line 97
    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/inbox/MessageState;->setRead(Z)V

    .line 98
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->notifyDataSetChanged()V

    return-void
.end method

.method public onRestoreInstanceState(Landroid/os/Parcelable;)V
    .locals 1

    if-nez p1, :cond_0

    return-void

    .line 227
    :cond_0
    instance-of v0, p1, Landroid/os/Bundle;

    if-eqz v0, :cond_1

    .line 228
    check-cast p1, Landroid/os/Bundle;

    const-string v0, "elements"

    .line 229
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getSerializable(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object v0

    check-cast v0, Ljava/util/ArrayList;

    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->elements:Ljava/util/ArrayList;

    const-string v0, "stateMap"

    .line 230
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getSerializable(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object p1

    check-cast p1, Ljava/util/HashMap;

    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->messageStateMap:Ljava/util/HashMap;

    :cond_1
    return-void
.end method

.method public onSaveInstanceState()Landroid/os/Parcelable;
    .locals 3

    .line 217
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "elements"

    .line 218
    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->elements:Ljava/util/ArrayList;

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    const-string v1, "stateMap"

    .line 219
    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->messageStateMap:Ljava/util/HashMap;

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    return-object v0
.end method

.method public setElements(Ljava/util/List;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/model/news/Article;",
            ">;)V"
        }
    .end annotation

    if-nez p1, :cond_0

    const/4 p1, 0x0

    .line 58
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->elements:Ljava/util/ArrayList;

    goto :goto_0

    .line 59
    :cond_0
    instance-of v0, p1, Ljava/util/ArrayList;

    if-eqz v0, :cond_1

    .line 60
    check-cast p1, Ljava/util/ArrayList;

    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->elements:Ljava/util/ArrayList;

    goto :goto_0

    .line 62
    :cond_1
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0, p1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->elements:Ljava/util/ArrayList;

    .line 65
    :goto_0
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    const-wide v2, 0x9a7ec800L

    sub-long/2addr v0, v2

    .line 66
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->messageStateManager:Lnet/gogame/gowrap/inbox/MessageStateManager;

    const-string v2, "news"

    invoke-interface {p1, v2, v0, v1}, Lnet/gogame/gowrap/inbox/MessageStateManager;->getMessageStates(Ljava/lang/String;J)Ljava/util/List;

    move-result-object p1

    .line 68
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    if-eqz p1, :cond_3

    .line 70
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_2
    :goto_1
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_3

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/inbox/MessageState;

    if-eqz v1, :cond_2

    .line 72
    invoke-virtual {v1}, Lnet/gogame/gowrap/inbox/MessageState;->getId()J

    move-result-wide v2

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v0, v2, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_1

    .line 76
    :cond_3
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->messageStateMap:Ljava/util/HashMap;

    .line 78
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_2/NewsListAdapter;->notifyDataSetChanged()V

    return-void
.end method
