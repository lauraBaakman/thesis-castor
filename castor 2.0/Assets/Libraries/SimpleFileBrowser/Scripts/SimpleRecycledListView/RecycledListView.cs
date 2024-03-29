﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleRecycledListView
{
	[RequireComponent( typeof( ScrollRect ) )]
	public class RecycledListView : MonoBehaviour
	{
		// Cached components
		public RectTransform viewportTransform;
		public RectTransform contentTransform;

		private float itemHeight, _1OverItemHeight;
		private float viewportHeight;

		private Dictionary<int, ListItem> items = new Dictionary<int, ListItem>();
		private Stack<ListItem> pooledItems = new Stack<ListItem>();

		IListViewAdapter adapter = null;

		// Current indices of items shown on screen
		private int currentTopIndex = -1, currentBottomIndex = -1;

		void Start()
		{
			viewportHeight = viewportTransform.rect.height;

			GetComponent<ScrollRect>().onValueChanged.AddListener( ( pos ) => UpdateItemsInTheList() );
		}

		public void SetAdapter( IListViewAdapter adapter )
		{
			this.adapter = adapter;

			itemHeight = adapter.ItemHeight;
			_1OverItemHeight = 1f / itemHeight;
		}

		// Update the list
		public void UpdateList()
		{
			float newHeight = Mathf.Max( 1f, adapter.Count * itemHeight );
			contentTransform.sizeDelta = new Vector2( 0f, newHeight );

			UpdateItemsInTheList( true );
		}

		// Window is resized, update the list
		public void OnViewportDimensionsChanged()
		{
			viewportHeight = viewportTransform.rect.height;

			UpdateItemsInTheList();
		}

		// Calculate the indices of items to show
		private void UpdateItemsInTheList( bool updateAllVisibleItems = false )
		{
			// If there is at least one item to show
			if( adapter.Count > 0 )
			{
				Vector3 localPos = contentTransform.localPosition;

				// Use an extra item at each side, in case of scrolling
				int newTopIndex = (int) ( localPos.y * _1OverItemHeight ) - 1;
				int newBottomIndex = (int) ( ( localPos.y + viewportHeight ) * _1OverItemHeight ) + 1;

				if( newTopIndex < 0 )
					newTopIndex = 0;

				if( newBottomIndex > adapter.Count - 1 )
					newBottomIndex = adapter.Count - 1;

				if( currentTopIndex == -1 )
				{
					// There are no active items

					updateAllVisibleItems = true;

					currentTopIndex = newTopIndex;
					currentBottomIndex = newBottomIndex;

					CreateItemsBetweenIndices( newTopIndex, newBottomIndex );
				}
				else
				{
					// There are some active items

					if( newBottomIndex < currentTopIndex || newTopIndex > currentBottomIndex )
					{
						// If user scrolled a lot such that, none of the items are now within
						// the bounds of the scroll view, pool all the previous items and create
						// new items for the new list of visible entries
						updateAllVisibleItems = true;

						DestroyItemsBetweenIndices( currentTopIndex, currentBottomIndex );
						CreateItemsBetweenIndices( newTopIndex, newBottomIndex );
					}
					else
					{
						// User did not scroll a lot such that, some items are are still within
						// the bounds of the scroll view. Don't destroy them but update their content,
						// if necessary
						if( newTopIndex > currentTopIndex )
						{
							DestroyItemsBetweenIndices( currentTopIndex, newTopIndex - 1 );
						}

						if( newBottomIndex < currentBottomIndex )
						{
							DestroyItemsBetweenIndices( newBottomIndex + 1, currentBottomIndex );
						}

						if( newTopIndex < currentTopIndex )
						{
							CreateItemsBetweenIndices( newTopIndex, currentTopIndex - 1 );

							// If it is not necessary to update all the items,
							// then just update the newly created items. Otherwise,
							// wait for the major update
							if( !updateAllVisibleItems )
							{
								UpdateItemContentsBetweenIndices( newTopIndex, currentTopIndex - 1 );
							}
						}

						if( newBottomIndex > currentBottomIndex )
						{
							CreateItemsBetweenIndices( currentBottomIndex + 1, newBottomIndex );

							// If it is not necessary to update all the items,
							// then just update the newly created items. Otherwise,
							// wait for the major update
							if( !updateAllVisibleItems )
							{
								UpdateItemContentsBetweenIndices( currentBottomIndex + 1, newBottomIndex );
							}
						}
					}

					currentTopIndex = newTopIndex;
					currentBottomIndex = newBottomIndex;
				}

				if( updateAllVisibleItems )
				{
					// Update all the items
					UpdateItemContentsBetweenIndices( currentTopIndex, currentBottomIndex );
				}
			}
			else if( currentTopIndex != -1 )
			{
				// There is nothing to show but some items are still visible; pool them
				DestroyItemsBetweenIndices( currentTopIndex, currentBottomIndex );

				currentTopIndex = -1;
			}
		}

		private void CreateItemsBetweenIndices( int topIndex, int bottomIndex )
		{
			for( int i = topIndex; i <= bottomIndex; i++ )
			{
				CreateItemAtIndex( i );
			}
		}

		// Create (or unpool) an item
		private void CreateItemAtIndex( int index )
		{
			ListItem item;
			if( pooledItems.Count > 0 )
			{
				item = pooledItems.Pop();
				item.gameObject.SetActive( true );
			}
			else
			{
				item = adapter.CreateItem();
				item.transform.SetParent( contentTransform, false );
				item.SetAdapter( adapter );
			}

			// Reposition the item
			item.transform.localPosition = new Vector3( 1f, -index * itemHeight, 0f );

			// To access this item easily in the future, add it to the dictionary
			items[index] = item;
		}

		private void DestroyItemsBetweenIndices( int topIndex, int bottomIndex )
		{
			for( int i = topIndex; i <= bottomIndex; i++ )
			{
				ListItem item = items[i];

				item.gameObject.SetActive( false );
				pooledItems.Push( item );
			}
		}

		private void UpdateItemContentsBetweenIndices( int topIndex, int bottomIndex )
		{
			for( int i = topIndex; i <= bottomIndex; i++ )
			{
				ListItem item = items[i];

				item.Position = i;
				adapter.SetItemContent( item );
			}
		}
	}
}