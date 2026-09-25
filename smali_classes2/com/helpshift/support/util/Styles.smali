.class public Lcom/helpshift/support/util/Styles;
.super Ljava/lang/Object;
.source "Styles.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 23
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getColor(Landroid/content/Context;I)I
    .locals 2

    .line 26
    invoke-virtual {p0}, Landroid/content/Context;->getTheme()Landroid/content/res/Resources$Theme;

    move-result-object p0

    const/4 v0, 0x1

    new-array v0, v0, [I

    const/4 v1, 0x0

    aput p1, v0, v1

    invoke-virtual {p0, v0}, Landroid/content/res/Resources$Theme;->obtainStyledAttributes([I)Landroid/content/res/TypedArray;

    move-result-object p0

    const/4 p1, -0x1

    .line 27
    invoke-virtual {p0, v1, p1}, Landroid/content/res/TypedArray;->getColor(II)I

    move-result p1

    .line 28
    invoke-virtual {p0}, Landroid/content/res/TypedArray;->recycle()V

    return p1
.end method

.method public static getInt(Landroid/content/Context;I)I
    .locals 2

    .line 33
    invoke-virtual {p0}, Landroid/content/Context;->getTheme()Landroid/content/res/Resources$Theme;

    move-result-object p0

    const/4 v0, 0x1

    new-array v0, v0, [I

    const/4 v1, 0x0

    aput p1, v0, v1

    invoke-virtual {p0, v0}, Landroid/content/res/Resources$Theme;->obtainStyledAttributes([I)Landroid/content/res/TypedArray;

    move-result-object p0

    .line 34
    invoke-virtual {p0, v1, v1}, Landroid/content/res/TypedArray;->getInt(II)I

    move-result p1

    .line 35
    invoke-virtual {p0}, Landroid/content/res/TypedArray;->recycle()V

    return p1
.end method

.method public static getQuestionWithHighlightedSearchTerms(Landroid/content/Context;Lcom/helpshift/support/Faq;Ljava/util/ArrayList;)Lcom/helpshift/support/Faq;
    .locals 18
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Lcom/helpshift/support/Faq;",
            "Ljava/util/ArrayList<",
            "Ljava/lang/String;",
            ">;)",
            "Lcom/helpshift/support/Faq;"
        }
    .end annotation

    move-object/from16 v0, p1

    if-eqz p2, :cond_e

    .line 72
    invoke-virtual/range {p2 .. p2}, Ljava/util/ArrayList;->size()I

    move-result v2

    if-lez v2, :cond_e

    .line 73
    invoke-static/range {p2 .. p2}, Ljava/util/Collections;->sort(Ljava/util/List;)V

    .line 74
    invoke-static/range {p2 .. p2}, Ljava/util/Collections;->reverse(Ljava/util/List;)V

    .line 76
    iget-object v2, v0, Lcom/helpshift/support/Faq;->title:Ljava/lang/String;

    .line 77
    iget-object v3, v0, Lcom/helpshift/support/Faq;->body:Ljava/lang/String;

    .line 79
    new-instance v4, Ljava/util/LinkedHashSet;

    invoke-direct {v4}, Ljava/util/LinkedHashSet;-><init>()V

    .line 80
    sget v5, Lcom/helpshift/R$attr;->hs__searchHighlightColor:I

    move-object/from16 v6, p0

    invoke-static {v6, v5}, Lcom/helpshift/util/Styles;->getHexColor(Landroid/content/Context;I)Ljava/lang/String;

    move-result-object v5

    .line 82
    invoke-static {v2}, Lcom/helpshift/support/util/HSTransliterator;->unidecode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v6, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_0

    .line 83
    invoke-static {v3}, Lcom/helpshift/support/util/HSTransliterator;->unidecode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v6, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_0

    const/4 v6, 0x1

    goto :goto_0

    :cond_0
    const/4 v6, 0x0

    :goto_0
    const/4 v9, 0x3

    if-nez v6, :cond_8

    .line 86
    invoke-virtual {v2}, Ljava/lang/String;->length()I

    move-result v6

    const-string v10, ""

    .line 89
    new-instance v11, Ljava/util/ArrayList;

    invoke-direct {v11}, Ljava/util/ArrayList;-><init>()V

    move-object v12, v10

    const/4 v10, 0x0

    :goto_1
    if-ge v10, v6, :cond_2

    .line 91
    invoke-virtual {v2, v10}, Ljava/lang/String;->charAt(I)C

    move-result v13

    .line 92
    new-instance v14, Ljava/lang/StringBuilder;

    invoke-direct {v14}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v14, v13}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const-string v13, ""

    invoke-virtual {v14, v13}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v14}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v13

    invoke-static {v13}, Lcom/helpshift/support/util/HSTransliterator;->unidecode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    move-object v14, v12

    const/4 v12, 0x0

    .line 93
    :goto_2
    invoke-virtual {v13}, Ljava/lang/String;->length()I

    move-result v15

    if-ge v12, v15, :cond_1

    .line 94
    new-instance v15, Ljava/lang/StringBuilder;

    invoke-direct {v15}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v15, v14}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v13, v12}, Ljava/lang/String;->charAt(I)C

    move-result v14

    invoke-virtual {v15, v14}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v15}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v14

    .line 95
    invoke-static {v10}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v15

    invoke-virtual {v11, v15}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    add-int/lit8 v12, v12, 0x1

    goto :goto_2

    :cond_1
    add-int/lit8 v10, v10, 0x1

    move-object v12, v14

    goto :goto_1

    .line 98
    :cond_2
    invoke-virtual {v12}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v6

    .line 100
    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v10

    .line 101
    invoke-static {v3}, Lcom/helpshift/support/util/HSTransliterator;->unidecode(Ljava/lang/String;)Ljava/lang/String;

    const-string v12, ""

    .line 104
    new-instance v13, Ljava/util/ArrayList;

    invoke-direct {v13}, Ljava/util/ArrayList;-><init>()V

    move-object v14, v12

    const/4 v12, 0x0

    :goto_3
    if-ge v12, v10, :cond_4

    .line 106
    invoke-virtual {v3, v12}, Ljava/lang/String;->charAt(I)C

    move-result v15

    .line 107
    new-instance v8, Ljava/lang/StringBuilder;

    invoke-direct {v8}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v8, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const-string v15, ""

    invoke-virtual {v8, v15}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v8}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v8

    invoke-static {v8}, Lcom/helpshift/support/util/HSTransliterator;->unidecode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    move-object v15, v14

    const/4 v14, 0x0

    .line 108
    :goto_4
    invoke-virtual {v8}, Ljava/lang/String;->length()I

    move-result v7

    if-ge v14, v7, :cond_3

    .line 109
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v7, v15}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v8, v14}, Ljava/lang/String;->charAt(I)C

    move-result v15

    invoke-virtual {v7, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v15

    .line 110
    invoke-static {v12}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v7

    invoke-virtual {v13, v7}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    add-int/lit8 v14, v14, 0x1

    goto :goto_4

    :cond_3
    add-int/lit8 v12, v12, 0x1

    move-object v14, v15

    goto :goto_3

    .line 113
    :cond_4
    invoke-virtual {v14}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v7

    .line 115
    invoke-virtual/range {p2 .. p2}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_5
    :goto_5
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v8

    if-eqz v8, :cond_a

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Ljava/lang/String;

    .line 116
    invoke-virtual {v8}, Ljava/lang/String;->length()I

    move-result v10

    if-lt v10, v9, :cond_7

    .line 117
    invoke-virtual {v8}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v8

    const/4 v10, 0x0

    .line 118
    invoke-static {v6, v8, v10}, Landroid/text/TextUtils;->indexOf(Ljava/lang/CharSequence;Ljava/lang/CharSequence;I)I

    move-result v12

    :goto_6
    if-ltz v12, :cond_6

    .line 120
    invoke-virtual {v11, v12}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v10

    check-cast v10, Ljava/lang/Integer;

    invoke-virtual {v10}, Ljava/lang/Integer;->intValue()I

    move-result v10

    .line 121
    invoke-virtual {v8}, Ljava/lang/String;->length()I

    move-result v14

    add-int/2addr v14, v12

    const/4 v15, 0x1

    sub-int/2addr v14, v15

    invoke-virtual {v11, v14}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v14

    check-cast v14, Ljava/lang/Integer;

    invoke-virtual {v14}, Ljava/lang/Integer;->intValue()I

    move-result v14

    add-int/2addr v14, v15

    .line 122
    invoke-virtual {v2, v10, v14}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v10

    .line 123
    invoke-virtual {v4, v10}, Ljava/util/LinkedHashSet;->add(Ljava/lang/Object;)Z

    .line 124
    invoke-virtual {v8}, Ljava/lang/String;->length()I

    move-result v10

    add-int/2addr v12, v10

    invoke-static {v6, v8, v12}, Landroid/text/TextUtils;->indexOf(Ljava/lang/CharSequence;Ljava/lang/CharSequence;I)I

    move-result v12

    goto :goto_6

    :cond_6
    const/4 v10, 0x0

    .line 127
    invoke-static {v7, v8, v10}, Landroid/text/TextUtils;->indexOf(Ljava/lang/CharSequence;Ljava/lang/CharSequence;I)I

    move-result v12

    :goto_7
    if-ltz v12, :cond_5

    .line 129
    invoke-virtual {v13, v12}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v14

    check-cast v14, Ljava/lang/Integer;

    invoke-virtual {v14}, Ljava/lang/Integer;->intValue()I

    move-result v14

    .line 130
    invoke-virtual {v8}, Ljava/lang/String;->length()I

    move-result v15

    add-int/2addr v15, v12

    const/16 v16, 0x1

    add-int/lit8 v15, v15, -0x1

    invoke-virtual {v13, v15}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v15

    check-cast v15, Ljava/lang/Integer;

    invoke-virtual {v15}, Ljava/lang/Integer;->intValue()I

    move-result v15

    add-int/lit8 v15, v15, 0x1

    .line 131
    invoke-virtual {v3, v14, v15}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v14

    .line 132
    invoke-virtual {v4, v14}, Ljava/util/LinkedHashSet;->add(Ljava/lang/Object;)Z

    .line 133
    invoke-virtual {v8}, Ljava/lang/String;->length()I

    move-result v14

    add-int/2addr v12, v14

    invoke-static {v7, v8, v12}, Landroid/text/TextUtils;->indexOf(Ljava/lang/CharSequence;Ljava/lang/CharSequence;I)I

    move-result v12

    goto :goto_7

    :cond_7
    const/4 v10, 0x0

    goto/16 :goto_5

    .line 139
    :cond_8
    invoke-virtual/range {p2 .. p2}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_9
    :goto_8
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v6

    if-eqz v6, :cond_a

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/String;

    .line 140
    invoke-virtual {v6}, Ljava/lang/String;->length()I

    move-result v7

    if-lt v7, v9, :cond_9

    .line 141
    invoke-virtual {v4, v6}, Ljava/util/LinkedHashSet;->add(Ljava/lang/Object;)Z

    goto :goto_8

    .line 145
    :cond_a
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, ">"

    invoke-virtual {v1, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "<"

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 146
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, ">"

    invoke-virtual {v3, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "<"

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    const-string v3, ">[^<]+<"

    .line 148
    invoke-static {v3}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v3

    .line 149
    invoke-virtual {v4}, Ljava/util/LinkedHashSet;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :goto_9
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v6

    if-eqz v6, :cond_d

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/String;

    .line 151
    invoke-virtual {v3, v2}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v7

    move-object v8, v2

    .line 152
    :goto_a
    invoke-virtual {v7}, Ljava/util/regex/Matcher;->find()Z

    move-result v9

    if-eqz v9, :cond_b

    .line 153
    invoke-virtual {v7}, Ljava/util/regex/Matcher;->start()I

    move-result v9

    invoke-virtual {v7}, Ljava/util/regex/Matcher;->end()I

    move-result v10

    invoke-virtual {v2, v9, v10}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v9

    .line 154
    new-instance v10, Ljava/lang/StringBuilder;

    invoke-direct {v10}, Ljava/lang/StringBuilder;-><init>()V

    const-string v11, "(?i)("

    invoke-virtual {v10, v11}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v11, ")"

    invoke-virtual {v10, v11}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v10

    new-instance v11, Ljava/lang/StringBuilder;

    invoke-direct {v11}, Ljava/lang/StringBuilder;-><init>()V

    const-string v12, "<span style=\"background-color: "

    invoke-virtual {v11, v12}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v11, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v12, "\">$1</span>"

    invoke-virtual {v11, v12}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v11}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v11

    invoke-virtual {v9, v10, v11}, Ljava/lang/String;->replaceAll(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v10

    .line 157
    invoke-virtual {v8, v9, v10}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v8

    goto :goto_a

    .line 161
    :cond_b
    invoke-virtual {v3, v1}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v2

    move-object v7, v1

    .line 162
    :goto_b
    invoke-virtual {v2}, Ljava/util/regex/Matcher;->find()Z

    move-result v9

    if-eqz v9, :cond_c

    .line 163
    invoke-virtual {v2}, Ljava/util/regex/Matcher;->start()I

    move-result v9

    invoke-virtual {v2}, Ljava/util/regex/Matcher;->end()I

    move-result v10

    invoke-virtual {v1, v9, v10}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v9

    .line 164
    new-instance v10, Ljava/lang/StringBuilder;

    invoke-direct {v10}, Ljava/lang/StringBuilder;-><init>()V

    const-string v11, "(?i)("

    invoke-virtual {v10, v11}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v11, ")"

    invoke-virtual {v10, v11}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v10}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v10

    new-instance v11, Ljava/lang/StringBuilder;

    invoke-direct {v11}, Ljava/lang/StringBuilder;-><init>()V

    const-string v12, "<span style=\"background-color: "

    invoke-virtual {v11, v12}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v11, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v12, "\">$1</span>"

    invoke-virtual {v11, v12}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v11}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v11

    invoke-virtual {v9, v10, v11}, Ljava/lang/String;->replaceAll(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v10

    .line 167
    invoke-virtual {v7, v9, v10}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v7

    goto :goto_b

    :cond_c
    move-object v1, v7

    move-object v2, v8

    goto/16 :goto_9

    .line 171
    :cond_d
    invoke-virtual {v2}, Ljava/lang/String;->length()I

    move-result v3

    const/4 v4, 0x1

    sub-int/2addr v3, v4

    invoke-virtual {v2, v4, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v12

    .line 172
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v2

    sub-int/2addr v2, v4

    invoke-virtual {v1, v4, v2}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v13

    .line 174
    new-instance v1, Lcom/helpshift/support/Faq;

    const-wide/16 v6, 0x1

    .line 175
    invoke-virtual/range {p1 .. p1}, Lcom/helpshift/support/Faq;->getId()Ljava/lang/String;

    move-result-object v8

    iget-object v9, v0, Lcom/helpshift/support/Faq;->publish_id:Ljava/lang/String;

    iget-object v10, v0, Lcom/helpshift/support/Faq;->language:Ljava/lang/String;

    iget-object v11, v0, Lcom/helpshift/support/Faq;->section_publish_id:Ljava/lang/String;

    iget v14, v0, Lcom/helpshift/support/Faq;->is_helpful:I

    iget-object v15, v0, Lcom/helpshift/support/Faq;->is_rtl:Ljava/lang/Boolean;

    .line 183
    invoke-virtual/range {p1 .. p1}, Lcom/helpshift/support/Faq;->getTags()Ljava/util/List;

    move-result-object v16

    .line 184
    invoke-virtual/range {p1 .. p1}, Lcom/helpshift/support/Faq;->getCategoryTags()Ljava/util/List;

    move-result-object v17

    move-object v5, v1

    invoke-direct/range {v5 .. v17}, Lcom/helpshift/support/Faq;-><init>(JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ILjava/lang/Boolean;Ljava/util/List;Ljava/util/List;)V

    goto :goto_c

    :cond_e
    const/4 v1, 0x0

    :goto_c
    return-object v1
.end method

.method public static getResourceIdForAttribute(Landroid/content/Context;I)I
    .locals 2

    .line 200
    new-instance v0, Landroid/util/TypedValue;

    invoke-direct {v0}, Landroid/util/TypedValue;-><init>()V

    .line 201
    invoke-virtual {p0}, Landroid/content/Context;->getTheme()Landroid/content/res/Resources$Theme;

    move-result-object p0

    const/4 v1, 0x1

    invoke-virtual {p0, p1, v0, v1}, Landroid/content/res/Resources$Theme;->resolveAttribute(ILandroid/util/TypedValue;Z)Z

    .line 202
    iget p0, v0, Landroid/util/TypedValue;->resourceId:I

    return p0
.end method

.method public static isTablet(Landroid/content/Context;)Z
    .locals 1

    .line 196
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p0

    sget v0, Lcom/helpshift/R$bool;->is_screen_large:I

    invoke-virtual {p0, v0}, Landroid/content/res/Resources;->getBoolean(I)Z

    move-result p0

    return p0
.end method

.method public static setAccentColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;)V
    .locals 1

    .line 66
    sget v0, Lcom/helpshift/R$attr;->colorAccent:I

    invoke-static {p0, p1, v0}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    return-void
.end method

.method public static setAdminChatBubbleColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;)V
    .locals 1

    .line 62
    sget v0, Lcom/helpshift/R$attr;->hs__chatBubbleAdminBackgroundColor:I

    invoke-static {p0, p1, v0}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    return-void
.end method

.method public static setImageAlpha(Landroid/widget/ImageButton;I)V
    .locals 2

    .line 40
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x10

    if-lt v0, v1, :cond_0

    .line 41
    invoke-virtual {p0, p1}, Landroid/widget/ImageButton;->setImageAlpha(I)V

    goto :goto_0

    .line 44
    :cond_0
    invoke-virtual {p0, p1}, Landroid/widget/ImageButton;->setAlpha(I)V

    :goto_0
    return-void
.end method

.method public static setSendMessageButtonIconColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;Z)V
    .locals 0

    if-eqz p2, :cond_0

    .line 53
    sget p2, Lcom/helpshift/R$attr;->colorAccent:I

    goto :goto_0

    :cond_0
    const p2, 0x101009a

    .line 58
    :goto_0
    invoke-static {p0, p1, p2}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    return-void
.end method
