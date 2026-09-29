.class public Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;
.super Landroid/app/Fragment;
.source "SupportFragment.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/VipListener;


# static fields
.field private static final AUTOCLOSING_EXPANDABLELISTVIEW_LISTENER_BUNDLE_NAME:Ljava/lang/String; = "autoClosingExpandableListViewListener"

.field private static final KEY_SHOW_BUTTONS:Ljava/lang/String; = "showButtons"

.field private static final LISTVIEW_BUNDLE_NAME:Ljava/lang/String; = "listView"

.field private static final LISTVIEW_BUNDLE_PROPERTY_NAME_STATE:Ljava/lang/String; = "state"

.field private static final SEARCH_PERIOD:J = 0x3e8L

.field private static final SEARCH_TERM_MIN_LENGTH:I = 0x1

.field private static final SEARCH_TEXT_FIELD_BUNDLE_NAME:Ljava/lang/String; = "searchTextField"

.field private static final SEARCH_TEXT_FIELD_BUNDLE_PROPERTY_NAME_TEXT:Ljava/lang/String; = "text"


# instance fields
.field private autoClosingExpandableListViewListener:Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;

.field private autoSearchRunnable:Ljava/lang/Runnable;

.field private chatButton:Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;

.field private currentQuery:Ljava/lang/String;

.field private currentSearchTerms:[Ljava/lang/String;

.field private expandableListView:Landroid/widget/ExpandableListView;

.field private handler:Landroid/os/Handler;

.field private listAdapter:Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;

.field private savedInstanceState:Landroid/os/Bundle;

.field private searchTextField:Landroid/widget/EditText;

.field private searchUpdating:Z

.field private showButtons:Z


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 40
    invoke-direct {p0}, Landroid/app/Fragment;-><init>()V

    const/4 v0, 0x0

    .line 63
    iput-boolean v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->searchUpdating:Z

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Landroid/widget/EditText;
    .locals 0

    .line 40
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->searchTextField:Landroid/widget/EditText;

    return-object p0
.end method

.method static synthetic access$100(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;Ljava/lang/String;)V
    .locals 0

    .line 40
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->search(Ljava/lang/String;)V

    return-void
.end method

.method static synthetic access$200(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Ljava/lang/Runnable;
    .locals 0

    .line 40
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->autoSearchRunnable:Ljava/lang/Runnable;

    return-object p0
.end method

.method static synthetic access$300(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Landroid/os/Handler;
    .locals 0

    .line 40
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->handler:Landroid/os/Handler;

    return-object p0
.end method

.method static synthetic access$400(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)V
    .locals 0

    .line 40
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->clearFocus()V

    return-void
.end method

.method static synthetic access$500(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Landroid/widget/ExpandableListView;
    .locals 0

    .line 40
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->expandableListView:Landroid/widget/ExpandableListView;

    return-object p0
.end method

.method static synthetic access$600(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Z
    .locals 0

    .line 40
    iget-boolean p0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->searchUpdating:Z

    return p0
.end method

.method static synthetic access$700(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;
    .locals 0

    .line 40
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->autoClosingExpandableListViewListener:Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;

    return-object p0
.end method

.method static synthetic access$800(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;
    .locals 0

    .line 40
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->listAdapter:Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;

    return-object p0
.end method

.method private clearFocus()V
    .locals 1

    .line 326
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->getView()Landroid/view/View;

    move-result-object v0

    invoke-virtual {v0}, Landroid/view/View;->clearFocus()V

    .line 327
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->hideSoftKeyboard(Landroid/app/Activity;)V

    return-void
.end method

.method public static create(Z)Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;
    .locals 3

    .line 66
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-direct {v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;-><init>()V

    .line 67
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "showButtons"

    .line 68
    invoke-virtual {v1, v2, p0}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    .line 69
    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->setArguments(Landroid/os/Bundle;)V

    return-object v0
.end method

.method private search(Ljava/lang/String;)V
    .locals 7

    .line 74
    invoke-static {p1}, Lnet/gogame/gowrap/support/StringUtils;->trimToNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 75
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->currentQuery:Ljava/lang/String;

    invoke-static {v0, p1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 78
    :cond_0
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->currentQuery:Ljava/lang/String;

    .line 80
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->listAdapter:Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;

    if-eqz p1, :cond_6

    const/4 p1, 0x1

    .line 81
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->searchUpdating:Z

    const/4 v0, 0x0

    .line 83
    :try_start_0
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->currentQuery:Ljava/lang/String;

    const-string v2, " "

    invoke-static {v1, v2}, Lnet/gogame/gowrap/support/StringUtils;->split(Ljava/lang/String;Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_4

    .line 85
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    .line 86
    array-length v3, v1

    const/4 v4, 0x0

    :goto_0
    if-ge v4, v3, :cond_2

    aget-object v5, v1, v4

    .line 87
    invoke-static {v5}, Lnet/gogame/gowrap/support/StringUtils;->trimToNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    if-eqz v5, :cond_1

    .line 88
    invoke-virtual {v5}, Ljava/lang/String;->length()I

    move-result v6

    if-lt v6, p1, :cond_1

    .line 89
    invoke-virtual {v5}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v5

    invoke-interface {v2, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_1
    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    .line 92
    :cond_2
    invoke-interface {v2}, Ljava/util/List;->isEmpty()Z

    move-result p1

    if-eqz p1, :cond_3

    const/4 v1, 0x0

    goto :goto_1

    .line 95
    :cond_3
    invoke-interface {v2}, Ljava/util/List;->size()I

    move-result p1

    new-array p1, p1, [Ljava/lang/String;

    invoke-interface {v2, p1}, Ljava/util/List;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object p1

    move-object v1, p1

    check-cast v1, [Ljava/lang/String;

    .line 98
    :cond_4
    :goto_1
    iput-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->currentSearchTerms:[Ljava/lang/String;

    if-eqz v1, :cond_5

    .line 100
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->expandableListView:Landroid/widget/ExpandableListView;

    invoke-virtual {p1, v0}, Landroid/widget/ExpandableListView;->expandGroup(I)Z

    .line 102
    :cond_5
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->listAdapter:Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;

    invoke-virtual {p1, v1}, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->setSearchTerms([Ljava/lang/String;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 104
    iput-boolean v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->searchUpdating:Z

    goto :goto_2

    :catchall_0
    move-exception p1

    iput-boolean v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->searchUpdating:Z

    .line 105
    throw p1

    :cond_6
    :goto_2
    return-void
.end method

.method private updateChatButton(ZZ)V
    .locals 1

    .line 370
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->chatButton:Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;

    if-eqz v0, :cond_1

    .line 371
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->chatButton:Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;

    if-nez p1, :cond_0

    if-nez p2, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;->setMasked(Z)V

    :cond_1
    return-void
.end method


# virtual methods
.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 9

    .line 112
    sget p3, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_fragment_support:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p3

    .line 115
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 116
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v1

    const-string v2, "showButtons"

    invoke-virtual {v1, v2, v0}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result v1

    iput-boolean v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->showButtons:Z

    .line 119
    :cond_0
    invoke-virtual {p2}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object p2

    .line 121
    instance-of v1, p2, Lnet/gogame/gowrap/ui/UIContext;

    const/4 v2, 0x0

    if-eqz v1, :cond_1

    .line 122
    move-object v1, p2

    check-cast v1, Lnet/gogame/gowrap/ui/UIContext;

    goto :goto_0

    :cond_1
    move-object v1, v2

    .line 126
    :goto_0
    sget v3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_back_button:I

    invoke-virtual {p3, v3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v3

    .line 127
    new-instance v4, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$1;

    invoke-direct {v4, p0, v1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$1;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;Lnet/gogame/gowrap/ui/UIContext;)V

    invoke-virtual {v3, v4}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 137
    new-instance v3, Landroid/os/Handler;

    invoke-direct {v3}, Landroid/os/Handler;-><init>()V

    iput-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->handler:Landroid/os/Handler;

    .line 138
    new-instance v3, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$2;

    invoke-direct {v3, p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$2;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)V

    iput-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->autoSearchRunnable:Ljava/lang/Runnable;

    .line 148
    sget v3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_faq_listview:I

    invoke-virtual {p3, v3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v3

    check-cast v3, Landroid/widget/ExpandableListView;

    iput-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->expandableListView:Landroid/widget/ExpandableListView;

    .line 151
    sget v3, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_include_faq_header:I

    iget-object v4, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->expandableListView:Landroid/widget/ExpandableListView;

    invoke-virtual {p1, v3, v4, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 153
    sget v3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_support_form_button:I

    invoke-virtual {p1, v3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v3

    if-eqz v3, :cond_2

    .line 156
    new-instance v4, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$3;

    invoke-direct {v4, p0, v1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$3;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;Lnet/gogame/gowrap/ui/UIContext;)V

    invoke-virtual {v3, v4}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 167
    :cond_2
    sget v3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_support_chat_button:I

    invoke-virtual {p1, v3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v3

    check-cast v3, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;

    iput-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->chatButton:Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;

    .line 169
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->chatButton:Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;

    if-eqz v3, :cond_3

    .line 170
    invoke-interface {v1}, Lnet/gogame/gowrap/ui/UIContext;->isVipChatEnabled()Z

    move-result v3

    sget-object v4, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v4}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isChatBotEnabled()Z

    move-result v4

    invoke-direct {p0, v3, v4}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->updateChatButton(ZZ)V

    .line 171
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->chatButton:Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;

    new-instance v4, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$4;

    invoke-direct {v4, p0, v1, p2}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$4;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;Lnet/gogame/gowrap/ui/UIContext;Landroid/content/Context;)V

    invoke-virtual {v3, v4}, Lnet/gogame/gowrap/ui/v2017_1/SupportCustomImageButton;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 192
    :cond_3
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->savedInstanceState:Landroid/os/Bundle;

    if-eqz v3, :cond_4

    .line 193
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->savedInstanceState:Landroid/os/Bundle;

    const-string v4, "searchTextField"

    invoke-virtual {v3, v4}, Landroid/os/Bundle;->getBundle(Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v3

    goto :goto_1

    :cond_4
    move-object v3, v2

    .line 196
    :goto_1
    sget v4, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_support_search_textfield:I

    invoke-virtual {p1, v4}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v4

    check-cast v4, Landroid/widget/EditText;

    iput-object v4, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->searchTextField:Landroid/widget/EditText;

    if-eqz v3, :cond_5

    .line 199
    iget-object v4, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->searchTextField:Landroid/widget/EditText;

    const-string v5, "text"

    invoke-virtual {v3, v5}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Landroid/widget/EditText;->setText(Ljava/lang/CharSequence;)V

    .line 202
    :cond_5
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->searchTextField:Landroid/widget/EditText;

    new-instance v4, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$5;

    invoke-direct {v4, p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$5;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)V

    invoke-virtual {v3, v4}, Landroid/widget/EditText;->setOnTouchListener(Landroid/view/View$OnTouchListener;)V

    .line 212
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->getActivity()Landroid/app/Activity;

    move-result-object v3

    iget-object v4, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->searchTextField:Landroid/widget/EditText;

    sget v5, Lnet/gogame/gowrap/R$array;->net_gogame_gowrap_search_edittext_drawables:I

    invoke-static {v3, v4, v5}, Lnet/gogame/gowrap/ui/utils/UIUtils;->setupRightDrawable(Landroid/content/Context;Landroid/widget/EditText;I)V

    .line 214
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->searchTextField:Landroid/widget/EditText;

    new-instance v4, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$6;

    invoke-direct {v4, p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$6;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)V

    invoke-virtual {v3, v4}, Landroid/widget/EditText;->setOnEditorActionListener(Landroid/widget/TextView$OnEditorActionListener;)V

    .line 223
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->searchTextField:Landroid/widget/EditText;

    new-instance v4, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7;

    invoke-direct {v4, p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)V

    invoke-virtual {v3, v4}, Landroid/widget/EditText;->setOnFocusChangeListener(Landroid/view/View$OnFocusChangeListener;)V

    .line 244
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->expandableListView:Landroid/widget/ExpandableListView;

    invoke-virtual {v3, p1}, Landroid/widget/ExpandableListView;->addHeaderView(Landroid/view/View;)V

    :try_start_0
    const-string v3, "net/gogame/gowrap/faq-article-template.html"

    const-string v4, "net/gogame/gowrap/faq-article-template-default.html"

    .line 248
    filled-new-array {v3, v4}, [Ljava/lang/String;

    move-result-object v3

    const-string v4, "UTF-8"

    invoke-static {p2, v3, v4}, Lnet/gogame/gowrap/io/utils/IOUtils;->assetToString(Landroid/content/Context;[Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_2

    :catch_0
    move-object v3, v2

    .line 257
    :goto_2
    sget-object v4, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v4, p2}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getCurrentLocale(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v4

    .line 258
    invoke-static {p2, v4}, Lnet/gogame/gowrap/support/FaqSupport;->getFaq(Landroid/content/Context;Ljava/lang/String;)Lnet/gogame/gowrap/model/faq/Category;

    move-result-object v4

    const/16 v5, 0x8

    if-nez v4, :cond_6

    .line 260
    new-instance v4, Lnet/gogame/gowrap/model/faq/Category;

    const-string v6, ""

    const-string v7, ""

    new-instance v8, Ljava/util/ArrayList;

    invoke-direct {v8}, Ljava/util/ArrayList;-><init>()V

    invoke-direct {v4, v6, v7, v8}, Lnet/gogame/gowrap/model/faq/Category;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/List;)V

    .line 261
    iget-object v6, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->searchTextField:Landroid/widget/EditText;

    invoke-virtual {v6, v5}, Landroid/widget/EditText;->setVisibility(I)V

    .line 264
    :cond_6
    new-instance v6, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;

    invoke-direct {v6, p2, v4}, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;-><init>(Landroid/content/Context;Lnet/gogame/gowrap/model/faq/Category;)V

    iput-object v6, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->listAdapter:Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;

    .line 265
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->listAdapter:Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;

    iget-object v4, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->currentSearchTerms:[Ljava/lang/String;

    invoke-virtual {p2, v4}, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->setSearchTerms([Ljava/lang/String;)V

    .line 267
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->savedInstanceState:Landroid/os/Bundle;

    if-eqz p2, :cond_7

    .line 268
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->savedInstanceState:Landroid/os/Bundle;

    const-string v4, "autoClosingExpandableListViewListener"

    .line 269
    invoke-virtual {p2, v4}, Landroid/os/Bundle;->getBundle(Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object p2

    goto :goto_3

    :cond_7
    move-object p2, v2

    .line 271
    :goto_3
    new-instance v4, Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;

    iget-object v6, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->expandableListView:Landroid/widget/ExpandableListView;

    invoke-direct {v4, v6, p2}, Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;-><init>(Landroid/widget/ExpandableListView;Landroid/os/Bundle;)V

    iput-object v4, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->autoClosingExpandableListViewListener:Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;

    .line 273
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->expandableListView:Landroid/widget/ExpandableListView;

    new-instance v4, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$8;

    invoke-direct {v4, p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$8;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)V

    invoke-virtual {p2, v4}, Landroid/widget/ExpandableListView;->setOnGroupExpandListener(Landroid/widget/ExpandableListView$OnGroupExpandListener;)V

    .line 283
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->expandableListView:Landroid/widget/ExpandableListView;

    iget-object v4, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->listAdapter:Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;

    invoke-virtual {p2, v4}, Landroid/widget/ExpandableListView;->setAdapter(Landroid/widget/ExpandableListAdapter;)V

    .line 284
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->expandableListView:Landroid/widget/ExpandableListView;

    new-instance v4, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$9;

    invoke-direct {v4, p0, v1, v3}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$9;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;Lnet/gogame/gowrap/ui/UIContext;Ljava/lang/String;)V

    invoke-virtual {p2, v4}, Landroid/widget/ExpandableListView;->setOnChildClickListener(Landroid/widget/ExpandableListView$OnChildClickListener;)V

    .line 304
    sget p2, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_back_support_buttons:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    .line 306
    iget-boolean p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->showButtons:Z

    if-eqz p2, :cond_8

    .line 307
    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    goto :goto_4

    .line 309
    :cond_8
    invoke-virtual {p1, v5}, Landroid/view/View;->setVisibility(I)V

    .line 313
    :goto_4
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->savedInstanceState:Landroid/os/Bundle;

    if-eqz p1, :cond_9

    .line 314
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->savedInstanceState:Landroid/os/Bundle;

    const-string p2, "listView"

    invoke-virtual {p1, p2}, Landroid/os/Bundle;->getBundle(Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v2

    :cond_9
    if-eqz v2, :cond_a

    const-string p1, "state"

    .line 317
    invoke-virtual {v2, p1}, Landroid/os/Bundle;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object p1

    .line 319
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->expandableListView:Landroid/widget/ExpandableListView;

    invoke-virtual {p2, p1}, Landroid/widget/ExpandableListView;->onRestoreInstanceState(Landroid/os/Parcelable;)V

    :cond_a
    return-object p3
.end method

.method public onDisableVipChat()V
    .locals 2

    .line 382
    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isChatBotEnabled()Z

    move-result v0

    const/4 v1, 0x0

    invoke-direct {p0, v1, v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->updateChatButton(ZZ)V

    return-void
.end method

.method public onEnableVipChat()V
    .locals 2

    .line 377
    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isChatBotEnabled()Z

    move-result v0

    const/4 v1, 0x1

    invoke-direct {p0, v1, v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->updateChatButton(ZZ)V

    return-void
.end method

.method public onPause()V
    .locals 4

    .line 340
    invoke-super {p0}, Landroid/app/Fragment;->onPause()V

    .line 342
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->handler:Landroid/os/Handler;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->autoSearchRunnable:Ljava/lang/Runnable;

    if-eqz v0, :cond_0

    .line 343
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->handler:Landroid/os/Handler;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->autoSearchRunnable:Ljava/lang/Runnable;

    invoke-virtual {v0, v1}, Landroid/os/Handler;->removeCallbacks(Ljava/lang/Runnable;)V

    .line 346
    :cond_0
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    .line 347
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->searchTextField:Landroid/widget/EditText;

    if-eqz v1, :cond_1

    .line 348
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "text"

    .line 349
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->searchTextField:Landroid/widget/EditText;

    .line 350
    invoke-virtual {v3}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v3

    .line 349
    invoke-virtual {v1, v2, v3}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "searchTextField"

    .line 351
    invoke-virtual {v0, v2, v1}, Landroid/os/Bundle;->putBundle(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 353
    :cond_1
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->autoClosingExpandableListViewListener:Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;

    if-eqz v1, :cond_2

    .line 354
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    .line 355
    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->autoClosingExpandableListViewListener:Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;

    invoke-virtual {v2, v1}, Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;->saveState(Landroid/os/Bundle;)V

    const-string v2, "autoClosingExpandableListViewListener"

    .line 357
    invoke-virtual {v0, v2, v1}, Landroid/os/Bundle;->putBundle(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 360
    :cond_2
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->expandableListView:Landroid/widget/ExpandableListView;

    if-eqz v1, :cond_3

    .line 361
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "state"

    .line 362
    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->expandableListView:Landroid/widget/ExpandableListView;

    .line 363
    invoke-virtual {v3}, Landroid/widget/ExpandableListView;->onSaveInstanceState()Landroid/os/Parcelable;

    move-result-object v3

    .line 362
    invoke-virtual {v1, v2, v3}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    const-string v2, "listView"

    .line 364
    invoke-virtual {v0, v2, v1}, Landroid/os/Bundle;->putBundle(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 366
    :cond_3
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->savedInstanceState:Landroid/os/Bundle;

    return-void
.end method

.method public onResume()V
    .locals 4

    .line 331
    invoke-super {p0}, Landroid/app/Fragment;->onResume()V

    .line 333
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->handler:Landroid/os/Handler;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->autoSearchRunnable:Ljava/lang/Runnable;

    if-eqz v0, :cond_0

    .line 334
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->handler:Landroid/os/Handler;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->autoSearchRunnable:Ljava/lang/Runnable;

    const-wide/16 v2, 0x3e8

    invoke-virtual {v0, v1, v2, v3}, Landroid/os/Handler;->postDelayed(Ljava/lang/Runnable;J)Z

    :cond_0
    return-void
.end method
