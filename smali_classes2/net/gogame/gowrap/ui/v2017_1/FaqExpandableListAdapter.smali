.class public Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;
.super Landroid/widget/BaseExpandableListAdapter;
.source "FaqExpandableListAdapter.java"


# instance fields
.field private final category:Lnet/gogame/gowrap/model/faq/Category;

.field private final context:Landroid/content/Context;

.field private final searchResultsCategory:Lnet/gogame/gowrap/model/faq/Category;

.field private terms:[Ljava/lang/String;


# direct methods
.method public constructor <init>(Landroid/content/Context;Lnet/gogame/gowrap/model/faq/Category;)V
    .locals 3

    .line 26
    invoke-direct {p0}, Landroid/widget/BaseExpandableListAdapter;-><init>()V

    .line 28
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->context:Landroid/content/Context;

    .line 29
    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->category:Lnet/gogame/gowrap/model/faq/Category;

    .line 31
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    sget p2, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_search_results_caption:I

    invoke-virtual {p1, p2}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 33
    new-instance p2, Lnet/gogame/gowrap/model/faq/Section;

    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    invoke-direct {p2, p1, v0}, Lnet/gogame/gowrap/model/faq/Section;-><init>(Ljava/lang/String;Ljava/util/List;)V

    .line 35
    new-instance v0, Lnet/gogame/gowrap/model/faq/Category;

    const/4 v1, 0x1

    new-array v1, v1, [Lnet/gogame/gowrap/model/faq/Section;

    const/4 v2, 0x0

    aput-object p2, v1, v2

    .line 36
    invoke-static {v1}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object p2

    invoke-direct {v0, p1, p1, p2}, Lnet/gogame/gowrap/model/faq/Category;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/List;)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->searchResultsCategory:Lnet/gogame/gowrap/model/faq/Category;

    return-void
.end method

.method private getDataSource()Lnet/gogame/gowrap/model/faq/Category;
    .locals 1

    .line 66
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->terms:[Ljava/lang/String;

    if-nez v0, :cond_0

    .line 67
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->category:Lnet/gogame/gowrap/model/faq/Category;

    return-object v0

    .line 69
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->searchResultsCategory:Lnet/gogame/gowrap/model/faq/Category;

    return-object v0
.end method


# virtual methods
.method public getChild(II)Ljava/lang/Object;
    .locals 1

    .line 100
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->getGroup(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gowrap/model/faq/Section;

    .line 101
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/faq/Section;->getArticles()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-ge p2, v0, :cond_0

    .line 102
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/faq/Section;->getArticles()Ljava/util/List;

    move-result-object p1

    invoke-interface {p1, p2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    :cond_0
    const/4 p1, 0x0

    return-object p1
.end method

.method public getChildId(II)J
    .locals 0

    int-to-long p1, p2

    return-wide p1
.end method

.method public getChildType(II)I
    .locals 1

    .line 149
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->getChildrenCount(I)I

    move-result p1

    const/4 v0, 0x1

    sub-int/2addr p1, v0

    if-ne p2, p1, :cond_0

    const/4 p1, 0x2

    return p1

    :cond_0
    if-nez p2, :cond_1

    const/4 p1, 0x0

    return p1

    :cond_1
    return v0
.end method

.method public getChildTypeCount()I
    .locals 1

    const/4 v0, 0x3

    return v0
.end method

.method public getChildView(IIZLandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 1

    .line 166
    invoke-virtual {p0, p1, p2}, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->getChild(II)Ljava/lang/Object;

    move-result-object p3

    check-cast p3, Lnet/gogame/gowrap/model/faq/Article;

    .line 168
    invoke-virtual {p0, p1, p2}, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->getChildType(II)I

    move-result p1

    if-nez p4, :cond_0

    .line 170
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->context:Landroid/content/Context;

    const-string v0, "layout_inflater"

    invoke-virtual {p2, v0}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Landroid/view/LayoutInflater;

    const/4 v0, 0x0

    packed-switch p1, :pswitch_data_0

    goto :goto_0

    .line 184
    :pswitch_0
    sget p1, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_fragment_faq_article_list_item_footer:I

    invoke-virtual {p2, p1, p5, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p4

    goto :goto_0

    .line 179
    :pswitch_1
    sget p1, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_fragment_faq_article_list_item:I

    invoke-virtual {p2, p1, p5, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p4

    goto :goto_0

    .line 174
    :pswitch_2
    sget p1, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_fragment_faq_article_list_item_header:I

    invoke-virtual {p2, p1, p5, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p4

    .line 193
    :cond_0
    :goto_0
    sget p1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_faq_article_title:I

    invoke-virtual {p4, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    if-eqz p3, :cond_1

    .line 196
    invoke-virtual {p3}, Lnet/gogame/gowrap/model/faq/Article;->getTitle()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    goto :goto_1

    .line 198
    :cond_1
    sget p2, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_faq_search_no_results_message:I

    invoke-virtual {p1, p2}, Landroid/widget/TextView;->setText(I)V

    :goto_1
    return-object p4

    nop

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public getChildrenCount(I)I
    .locals 1

    .line 90
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->getGroup(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gowrap/model/faq/Section;

    .line 91
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->terms:[Ljava/lang/String;

    if-eqz v0, :cond_1

    invoke-virtual {p1}, Lnet/gogame/gowrap/model/faq/Section;->getArticles()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x1

    return p1

    .line 92
    :cond_1
    :goto_0
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/faq/Section;->getArticles()Ljava/util/List;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result p1

    return p1
.end method

.method public getGroup(I)Ljava/lang/Object;
    .locals 1

    .line 80
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->getDataSource()Lnet/gogame/gowrap/model/faq/Category;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/faq/Category;->getSections()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public getGroupCount()I
    .locals 1

    .line 75
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->getDataSource()Lnet/gogame/gowrap/model/faq/Category;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/faq/Category;->getSections()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    return v0
.end method

.method public getGroupId(I)J
    .locals 2

    int-to-long v0, p1

    return-wide v0
.end method

.method public getGroupView(IZLandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 1

    .line 126
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->getGroup(I)Ljava/lang/Object;

    move-result-object p3

    check-cast p3, Lnet/gogame/gowrap/model/faq/Section;

    const/4 v0, 0x0

    if-eqz p2, :cond_0

    .line 127
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->getChildrenCount(I)I

    move-result p1

    if-lez p1, :cond_0

    .line 128
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->context:Landroid/content/Context;

    const-string p2, "layout_inflater"

    invoke-virtual {p1, p2}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/view/LayoutInflater;

    .line 130
    sget p2, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_fragment_faq_section_expanded_list_item:I

    invoke-virtual {p1, p2, p4, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    goto :goto_0

    .line 134
    :cond_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->context:Landroid/content/Context;

    const-string p2, "layout_inflater"

    invoke-virtual {p1, p2}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/view/LayoutInflater;

    .line 136
    sget p2, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_fragment_faq_section_list_item:I

    invoke-virtual {p1, p2, p4, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 140
    :goto_0
    sget p2, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_faq_section_name:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/TextView;

    .line 142
    invoke-virtual {p3}, Lnet/gogame/gowrap/model/faq/Section;->getName()Ljava/lang/String;

    move-result-object p3

    invoke-virtual {p2, p3}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    return-object p1
.end method

.method public hasStableIds()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public isChildSelectable(II)Z
    .locals 0

    const/4 p1, 0x1

    return p1
.end method

.method public setSearchTerms([Ljava/lang/String;)V
    .locals 11

    .line 40
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->terms:[Ljava/lang/String;

    .line 41
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->searchResultsCategory:Lnet/gogame/gowrap/model/faq/Category;

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/faq/Category;->getSections()Ljava/util/List;

    move-result-object v0

    const/4 v1, 0x0

    invoke-interface {v0, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/model/faq/Section;

    .line 42
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/faq/Section;->getArticles()Ljava/util/List;

    move-result-object v2

    invoke-interface {v2}, Ljava/util/List;->clear()V

    if-eqz p1, :cond_7

    .line 44
    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->category:Lnet/gogame/gowrap/model/faq/Category;

    invoke-virtual {v2}, Lnet/gogame/gowrap/model/faq/Category;->getSections()Ljava/util/List;

    move-result-object v2

    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :cond_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_7

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lnet/gogame/gowrap/model/faq/Section;

    .line 45
    invoke-virtual {v3}, Lnet/gogame/gowrap/model/faq/Section;->getArticles()Ljava/util/List;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :cond_1
    :goto_0
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_0

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lnet/gogame/gowrap/model/faq/Article;

    .line 46
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/faq/Article;->getTitle()Ljava/lang/String;

    move-result-object v5

    const/4 v6, 0x0

    if-eqz v5, :cond_2

    .line 47
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/faq/Article;->getTitle()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v5

    goto :goto_1

    :cond_2
    move-object v5, v6

    .line 48
    :goto_1
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/faq/Article;->getBody()Ljava/lang/String;

    move-result-object v7

    if-eqz v7, :cond_3

    .line 49
    invoke-virtual {v4}, Lnet/gogame/gowrap/model/faq/Article;->getBody()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v6}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v6

    .line 50
    :cond_3
    array-length v7, p1

    const/4 v8, 0x0

    :goto_2
    if-ge v8, v7, :cond_1

    aget-object v9, p1, v8

    if-eqz v9, :cond_6

    if-eqz v5, :cond_4

    .line 52
    invoke-virtual {v5, v9}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v10

    if-nez v10, :cond_5

    :cond_4
    if-eqz v6, :cond_6

    .line 53
    invoke-virtual {v6, v9}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v9

    if-eqz v9, :cond_6

    .line 54
    :cond_5
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/faq/Section;->getArticles()Ljava/util/List;

    move-result-object v5

    invoke-interface {v5, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_6
    add-int/lit8 v8, v8, 0x1

    goto :goto_2

    .line 62
    :cond_7
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->notifyDataSetChanged()V

    return-void
.end method
